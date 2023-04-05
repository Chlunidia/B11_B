using System;
using System.Data;
using System.Data.SqlClient;

namespace DaftarBimbel
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi ke database\n");
                    Console.WriteLine("Masukkan user ID: ");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan password: ");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan: ");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk terhubung ke database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = CHLUNIDIA; " + "initial catalog = {0}; " + "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat seluruh data");
                                        Console.WriteLine("2. Tambah data");
                                        Console.WriteLine("3. Hapus data");
                                        Console.WriteLine("4. Keluar");
                                        Console.Write("\nEnter your choice (1-3): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA MAHASISWA\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                    conn.Close();
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA MAHASISWA\n");
                                                    Console.WriteLine("Masukkan NIM: ");
                                                    string NIM = Console.ReadLine();
                                                    Console.WriteLine("Masukkan nama mahasiswa: ");
                                                    string NmaMhs = Console.ReadLine();
                                                    Console.WriteLine("Masukkan alamat mahasiswa: ");
                                                    string Almt = Console.ReadLine();
                                                    Console.WriteLine("Masukkan jenis kelamin (L/P): ");
                                                    string jk = Console.ReadLine();
                                                    Console.WriteLine("Masukkan nomor telepon: ");
                                                    string notlpn = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(NIM, NmaMhs, Almt, jk, notlpn, conn);
                                                        conn.Close();
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAlamat tidak memiliki " +
                                                            "akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak dapat mengakses database menggunakan user tersebut\n");
                    Console.ResetColor();
                }
            }
        }
        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * from HRD.Mahasiswa", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
        }
        public void insert(string NIM, string NmaMhs, string Almt, string jk, string notlpn, SqlConnection con)
        {
            string str = "";
            str = "insert into HRD.MAHASISWA (NIM, NamaMhs, AlamatMhs, Sex, PhoneMhs)" + " values(@nim, @nma, @alamat, @JK, @Phn)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("nim", NIM));
            cmd.Parameters.Add(new SqlParameter("nma", NmaMhs));
            cmd.Parameters.Add(new SqlParameter("alamat", Almt));
            cmd.Parameters.Add(new SqlParameter("JK", jk));
            cmd.Parameters.Add(new SqlParameter("Phn", notlpn));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data berhasil ditambahkan");
        }
    }
}
