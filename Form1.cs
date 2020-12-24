using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace loki97
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ulong[] K1 = new ulong[49];
        private ulong[] K2 = new ulong[49];
        private ulong[] K3 = new ulong[49];
        private ulong[] K4 = new ulong[49];
        private ulong[] SK = new ulong[49];
        private ulong[] R = new ulong[17];
        private ulong[] L = new ulong[17];
        private void encode_Click(object sender, EventArgs e)
        {
            int keylength = key.Text.Length;
            if (keylength > 32 || keylength < 9)
            {
                string message = "Неправильная длинна ключа!";
                string caption = "Ключ должен содержать от 16 до 32 знаков в шестнадцатиричной системе счисления!";
                _ = MessageBox.Show(caption, message, MessageBoxButtons.OK);
                return;
            }
            K1[0] = Convert.ToUInt64(key.Text.Substring(0, 8),16);
            K2[0] = Convert.ToUInt64(key.Text.Substring(8, keylength-8>=8?8:keylength-8),16);
            if (keylength < 17 && keylength > 8)
            {
                K3[0] = f_function(K1[0], K2[0]);
                K4[0] = f_function(K2[0], K1[0]);
            }else if(keylength < 25 || keylength > 16)
            {
                K3[0] = Convert.ToUInt64(key.Text.Substring(16, keylength - 16 >= 8 ? 8 : keylength - 16),16);
                K4[0] = f_function(K2[0], K1[0]);
            }
            else
            {
                K3[0] = Convert.ToUInt64(key.Text.Substring(16, keylength - 16 >= 8 ? 8 : keylength - 16),16);
                K4[0] = Convert.ToUInt64(key.Text.Substring(24, keylength - 24 >= 8 ? 8 : keylength - 24),16);
            }
            ResolveKeys();

            decodedBytes.Text = "";
            encodedBytes.Text = "";
            encodedText.Text = "";

            var DecodedText = decodedText.Text;
            var decodedB = Encoding.UTF8.GetBytes(DecodedText);
            foreach(var b in decodedB)
            {
                decodedBytes.Text +=" "+ Convert.ToString(b,2);
            }
            var blocks = new List<ulong>();
            var codedblocks = new List<ulong>();
            for (int i = 0; i < (int)Math.Ceiling(decodedB.Length / 16d); i++)
            {
                string blockL = "";
                string blockR = "";
                int counter = 0;
                for(int j = i * 16; j < (i + 1) * 16; j++)
                {
                    byte currentbyte = (j < decodedB.Length) ? decodedB[j] : (byte)32;
                    string current = Convert.ToString(currentbyte, 2);
                    if (counter < 8)
                    {
                        for (int k = 0; k < 8 - current.Length; k++)
                        {
                            blockL += "0";
                        }
                        blockL += current;
                    }
                    else
                    {
                        for (int k = 0; k < 8 - current.Length; k++)
                        {
                            blockR += "0";
                        }
                        blockR += current;
                    }
                    counter++;
                }
                blocks.Add(Convert.ToUInt64(blockL,2));
                blocks.Add(Convert.ToUInt64(blockR,2));
            }
            for(int g = 0; g < (int)Math.Truncate(blocks.Count / 2d); g+=2)
            {
                L[0] = blocks[g];
                R[0] = blocks[g+1];
                for (int i = 1; i < 17; i++)
                {
                    R[i] = L[i - 1] ^ f_function(R[i - 1] + SK[3 * i - 2], SK[3 * i - 1]);
                    L[i] = R[i - 1] + SK[3 * i - 2] + SK[3 * i];
                }
                codedblocks.Add(L[16]);
                codedblocks.Add(R[16]);
            }
            var encodedBytesList = new List<byte>();
            foreach(var block in codedblocks)
            {
                var block_string = Convert.ToString((long)block, 2);
                block_string = block_string.Insert(block_string.Length, Multiply("0", 64 - block_string.Length));
                for (int d = 0; d < 8; d++)
                {
                    var byte_string = block_string.Substring(d * 8, 8);
                    var byte_value = Convert.ToByte(byte_string,2);
                    encodedBytes.Text += byte_string + " ";
                    encodedBytesList.Add(byte_value);
                }
            }
            encodedText.Text = Encoding.UTF8.GetString(encodedBytesList.ToArray());
        }

        private void ResolveKeys()
        {
            for(ulong i = 1; i < 49; i++)
            {
                SK[i] = K4[i - 1] ^ g_function(K1[i - 1], K3[i - 1], K2[i - 1], i);
                K1[i] = SK[i];
                K2[i] = K1[i - 1];
                K3[i] = K2[i - 1];
                K4[i] = K3[i - 1];
            }
        }
        private ulong g_function(ulong k1,ulong k3, ulong k2,ulong i)
        {
            return f_function(k1 + k3 + (0x9E3779B97F4A7C15 * i), k2);
        }

        private ulong KP(ulong A, ulong B)
        {
            var A_chars = Convert.ToString((long)A, 2).ToCharArray();
            int previousSize = A_chars.Length;
            Array.Resize(ref A_chars, 64);
            for (int i = previousSize; i < A_chars.Length; i++)
            {
                A_chars[i] = '0';
            }
            var B_chars = Convert.ToString((long)B, 2).ToCharArray();
            for(int i = 0; i < 32; i++)
            {
                var cheker = i < B_chars.Length?B_chars[i]:'0';
                if (cheker=='1')
                {
                    A_chars[i] = A_chars[i] == '1' ? '0' : '1';
                    A_chars[i+32] = A_chars[i+32] == '1' ? '0' : '1';
                }
            }
            var A_s = new string(A_chars);
            var B_s = new string(B_chars);
            return Convert.ToUInt64(A_s, 2);
        }

        private string E(ulong x)
        {
            //[4-0|63-56|58-48|52-40|42-32|34-24|28-16|18-8|12-0]
            var binaryArray = Convert.ToString((long)x, 2);
            binaryArray = binaryArray.Insert(0, Multiply("0",64 - binaryArray.Length));
            var newValue = new List<bool>();
            for (int i = 4; i >= 0; i--)  newValue.Add(binaryArray[i]=='1');
            for (int i = 63; i >= 56; i--) newValue.Add(binaryArray[i] == '1');
            for (int i = 58; i >= 48; i--) newValue.Add(binaryArray[i] == '1');
            for (int i = 52; i >= 40; i--) newValue.Add(binaryArray[i] == '1');
            for (int i = 42; i >= 32; i--) newValue.Add(binaryArray[i] == '1');
            for (int i = 34; i >= 24; i--) newValue.Add(binaryArray[i] == '1');
            for (int i = 28; i >= 16; i--) newValue.Add(binaryArray[i] == '1');
            for (int i = 18; i >= 8; i--) newValue.Add(binaryArray[i] == '1');
            for (int i = 12; i >= 0; i--) newValue.Add(binaryArray[i] == '1');
            var xs = "";
            foreach(var b in newValue)
            {
                xs += b ? "1" : "0";
            }
            return xs;
        }
        private ulong Sa(string binaryValue)
        {
            ulong result=0;
            result += S1(Convert.ToUInt64(binaryValue.Substring(0, 13),2));
            result += S2(Convert.ToUInt64(binaryValue.Substring(13, 11),2));
            result += S1(Convert.ToUInt64(binaryValue.Substring(24, 13),2));
            result += S2(Convert.ToUInt64(binaryValue.Substring(37, 11),2));
            result += S2(Convert.ToUInt64(binaryValue.Substring(48, 11),2));
            result += S1(Convert.ToUInt64(binaryValue.Substring(59, 13),2));
            result += S2(Convert.ToUInt64(binaryValue.Substring(72, 11),2));
            result += S1(Convert.ToUInt64(binaryValue.Substring(83, 13),2));
            return result;
        }
        private ulong Sb(string binaryV)
        {
            ulong result = 0;
            var binaryValue = binaryV.Insert(0, Multiply("0", 96 - binaryV.Length));
            result += S2(Convert.ToUInt64(binaryValue.Substring(0, 13),2));
            result += S2(Convert.ToUInt64(binaryValue.Substring(13, 13),2));
            result += S1(Convert.ToUInt64(binaryValue.Substring(26, 11),2));
            result += S1(Convert.ToUInt64(binaryValue.Substring(37, 11),2));
            result += S2(Convert.ToUInt64(binaryValue.Substring(48, 13),2));
            result += S2(Convert.ToUInt64(binaryValue.Substring(61, 13),2));
            result += S1(Convert.ToUInt64(binaryValue.Substring(74, 11),2));
            result += S1(Convert.ToUInt64(binaryValue.Substring(85, 11),2));
            return result;
        }

        private ulong S1(ulong x)
        {
            var a = x ^ 0x1FFF;
            a = a * a * a;
            a = (a % 0x2911) & 0xFF;
            return a;
        }
        private ulong S2(ulong x)
        {
            var a = x ^ 0x7FF;
            a = a * a * a;
            a = (a % 0xAA7) & 0xFF;
            return a;
        }

        private ulong f_function(ulong A, ulong B)
        {
            return Sb(Convert.ToString((long)P(Sa(E(KP(A, B)))), 2));
        }

        private ulong P(ulong x)
        {
            string v = Convert.ToString((long)x, 2);
            v = v.Insert(0, Multiply("0", 64 - v.Length));
            string result = "";
            int[] a =
            {56,48,40,32,24,16,08,00,57,49,41,33,25,17,09,01,
             58,50,42,34,26,18,10,02,59,51,43,35,27,19,11,03,
             60,52,44,36,28,20,12,04,61,53,45,37,29,21,13,05,
             62,54,46,38,30,22,14,06,63,55,47,39,31,23,15,07};
            for(int i = 63; i >= 0; i--)
            {
                result += v[a[i]];
            }
            return Convert.ToUInt64(result,2);
        }
        public static string Multiply(string source, int multiplier)
        {
            StringBuilder sb = new StringBuilder(multiplier * source.Length);
            for (int i = 0; i < multiplier; i++)
            {
                sb.Append(source);
            }

            return sb.ToString();
        }

        private void decode_Click(object sender, EventArgs e)
        {
            int keylength = key.Text.Length;
            if (keylength > 32 || keylength < 9)
            {
                string message = "Неправильная длинна ключа!";
                string caption = "Ключ должен содержать от 16 до 32 знаков в шестнадцатиричной системе счисления!";
                _ = MessageBox.Show(caption, message, MessageBoxButtons.OK);
                return;
            }
            K1[0] = Convert.ToUInt64(key.Text.Substring(0, 8), 16);
            K2[0] = Convert.ToUInt64(key.Text.Substring(8, keylength - 8 >= 8 ? 8 : keylength - 8), 16);
            if (keylength < 17 && keylength > 8)
            {
                K3[0] = f_function(K1[0], K2[0]);
                K4[0] = f_function(K2[0], K1[0]);
            }
            else if (keylength < 25 || keylength > 16)
            {
                K3[0] = Convert.ToUInt64(key.Text.Substring(16, keylength - 16 >= 8 ? 8 : keylength - 16), 16);
                K4[0] = f_function(K2[0], K1[0]);
            }
            else
            {
                K3[0] = Convert.ToUInt64(key.Text.Substring(16, keylength - 16 >= 8 ? 8 : keylength - 16), 16);
                K4[0] = Convert.ToUInt64(key.Text.Substring(24, keylength - 24 >= 8 ? 8 : keylength - 24), 16);
            }
            ResolveKeys();

            decodedBytes.Text = "";
            encodedBytes.Text = "";
            decodedText.Text = "";

            var EncodedText = encodedText.Text;
            var encodedB = Encoding.UTF8.GetBytes(EncodedText);
            foreach (var b in encodedB)
            {
                encodedBytes.Text += " " + Convert.ToString(b, 2);
            }
            var encodedblocks = new List<ulong>();
            var decodedblocks = new List<ulong>();
            for (int i = 0; i < (int)Math.Ceiling(encodedB.Length / 16d); i++)
            {
                string blockL = "";
                string blockR = "";
                int counter = 0;
                for (int j = i * 16; j < (i + 1) * 16; j++)
                {
                    byte currentbyte = (j < encodedB.Length) ? encodedB[j] : (byte)32;
                    string current = Convert.ToString(currentbyte, 2);
                    if (counter < 8)
                    {
                        for (int k = 0; k < 8 - current.Length; k++)
                        {
                            blockL += "0";
                        }
                        blockL += current;
                    }
                    else
                    {
                        for (int k = 0; k < 8 - current.Length; k++)
                        {
                            blockR += "0";
                        }
                        blockR += current;
                    }
                    counter++;
                }
                encodedblocks.Add(Convert.ToUInt64(blockL, 2));
                encodedblocks.Add(Convert.ToUInt64(blockR, 2));
            }
            for (int g = 0; g < (int)Math.Truncate(encodedblocks.Count / 2d); g += 2)
            {
                L[16] = encodedblocks[g];
                R[16] = encodedblocks[g + 1];
                for (int i = 16; i >= 1; i--)
                {
                    R[i-1] = L[i] ^ f_function(R[i] + SK[3 * i], SK[3 * i - 1]);
                    L[i-1] = R[i] + SK[3 * i] + SK[3 * i - 2];
                }
                decodedblocks.Add(L[0]);
                decodedblocks.Add(R[0]);
            }
            var decodedBytesList = new List<byte>();
            foreach (var block in decodedblocks)
            {
                var block_string = Convert.ToString((long)block, 2);
                block_string = block_string.Insert(block_string.Length, Multiply("0", 64 - block_string.Length));
                for (int d = 0; d < 8; d++)
                {
                    var byte_string = block_string.Substring(d * 8, 8);
                    var byte_value = Convert.ToByte(byte_string, 2);
                    decodedBytes.Text += byte_string + " ";
                    decodedBytesList.Add(byte_value);
                }
            }
            decodedText.Text = Encoding.UTF8.GetString(decodedBytesList.ToArray());
        }
    }
}
