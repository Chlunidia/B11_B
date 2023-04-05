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
                                                    Console.WriteLine("INPUT DATA PENDAFTARAN\n");
                                                    Console.WriteLine("Masukkan ID Pendaftaran (example : P001): ");
                                                    string id_pen = Console.ReadLine();
                                                    Console.WriteLine("Masukkan ID Siswa (example : S001): ");
                                                    string id_sis = Console.ReadLine();
                                                    Console.WriteLine("Masukkan ID Kelas (example : K001): ");
                                                    string id_kel = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Tanggal Pendaftaran (example : 2023-04-05): ");
                                                    string tgl = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert( id_pen, id_sis, id_kel, tgl , conn);
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
                                                Console.Clear();
                                                Console.WriteLine("INPUT DATA YANG INGIN DIHAPUS\n");
                                                Console.WriteLine("Masukkan ID Pendaftaran (example : P001): ");
                                                string id_pens = Console.ReadLine();
                                                try
                                                {
                                                    pr.delete(id_pens, conn);
                                                    conn.Close();
                                                }
                                                catch
                                                {
                                                    Console.WriteLine("\nAlamat tidak memiliki " +
                                                        "akses untuk menambah data");
                                                }
                                                break;
                                            case '4':
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
            SqlCommand cmd = new SqlCommand("Select * from pendaftaran", con);
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
        public void insert(string id_pen, string id_sis, string id_kel, string tgl, SqlConnection con)
        {
            string str = "";
            str = "insert into pendaftaran (id_pendaftaran, id_siswa, id_kelas, tanggal_pendaftaran)" + " values(@idpen, @idsis, @idkel, @tanggal)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("idpen", id_pen));
            cmd.Parameters.Add(new SqlParameter("idsis", id_sis));
            cmd.Parameters.Add(new SqlParameter("idkel", id_kel));
            cmd.Parameters.Add(new SqlParameter("tanggal", tgl));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data berhasil ditambahkan");
        }
        public void delete(string id_pens, SqlConnection con)
        {
            string str = "";
            str = "delete from pendaftaran @idpen";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("idpen", id_pens));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data berhasil ditambahkan");
        }
    }
}
