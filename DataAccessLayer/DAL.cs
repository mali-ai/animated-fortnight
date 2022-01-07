using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace DataAccessLayer
{
    public class DAL
    {
        public static string connStr = @"Data Source=DESKTOP-55EDR72\SQLEXPRESS;Initial Catalog=Assignment5;Integrated Security=True";
        public static User isUserPresent(string login, string password)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT * from [User] WHERE Login = @param1 AND Password = @param2";
                    cmd.Parameters.AddWithValue("@param1", login);
                    cmd.Parameters.AddWithValue("@param2", password);

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            return getUserbyID(Convert.ToInt32(reader[0]));
                        }

                        return null;
                    }
                    catch (SqlException e)
                    {
                        return null;
                    }
                }
            }
        }

        public static User getUserbyID(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT * from [User] WHERE UserId = @param1";
                    cmd.Parameters.AddWithValue("@param1", id);
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            User user = new User();
                            user.userId = reader.GetInt32(0);
                            user.login = reader.GetString(1);
                            user.name = reader.GetString(2);
                            user.password = reader.GetString(3);
                            user.createdOn = reader.GetDateTime(4);
                            user.isActive = reader.GetBoolean(5);
                            user.picURL = reader.GetString(6);
                            user.isAdmin = reader.GetBoolean(7);

                            return user;
                        }
                        return null;
                    }
                    catch (SqlException e)
                    {
                        return null;
                    }
                }
            }
        }

        public static Product getProductById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT * from Product WHERE ProductId = @param1";
                    cmd.Parameters.AddWithValue("@param1", id);
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            Product product = new Product();
                            product.productId = reader.GetInt32(0);
                            product.name = reader.GetString(1);
                            product.typeId = reader.GetInt32(2);
                            product.price = reader.GetDouble(3);
                            product.description = reader.GetString(4);
                            product.picURL = reader.GetString(5);
                            product.updatedOn = reader.GetDateTime(6);
                            product.updatedBy = reader.GetInt32(7);
                            product.isActive = reader.GetBoolean(8);
                            product.typeName = getTypeNameById(product.typeId);
                            return product;
                        }
                        return null;
                    }
                    catch (SqlException e)
                    {
                        return null;
                    }
                }
            }
        }

        public static List<String> getAllLogins()
        {
            List<String> list = new List<String>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT Login FROM [User]";
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                        }
                        return list;
                    }
                    catch (SqlException e)
                    {
                        return null;
                    }
                }
            }
        }


        public static List<String> getAllProductNames()
        {
            List<String> list = new List<String>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT Name FROM Product";
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                        }
                        return list;
                    }
                    catch (SqlException e)
                    {
                        return null;
                    }
                }
            }
        }

        public static List<Product> getAllProducts()
        {
            List<Product> list = new List<Product>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT * from Product WHERE IsActive = 1";
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Product product = new Product();
                            product.productId = reader.GetInt32(0);
                            product.name = reader.GetString(1);
                            product.typeId = reader.GetInt32(2);
                            product.price = reader.GetDouble(3);
                            product.description = reader.GetString(4);
                            product.picURL = reader.GetString(5);
                            product.updatedOn = reader.GetDateTime(6);
                            product.updatedBy = reader.GetInt32(7);
                            product.isActive = reader.GetBoolean(8);
                            product.typeName = getTypeNameById(product.typeId);
                            product.adminName = ((User)getUserbyID(product.updatedBy)).name;
                            list.Add(product);

                        }
                        return list;
                    }
                    catch (SqlException e)
                    {
                        return null;
                    }
                }
            }
        }

        public static List<Types> getAllTypes()
        {
            List<Types> list = new List<Types>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT * from Type";
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Types type = new Types();
                            type.typeId = reader.GetInt32(0);
                            type.typeName = reader.GetString(1);
                            list.Add(type);

                        }
                        return list;
                    }
                    catch (SqlException e)
                    {
                        return null;
                    }
                }
            }
        }



        public static String getTypeNameById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT TypeName from Type WHERE TypeId = @param1";
                    cmd.Parameters.AddWithValue("@param1", id);
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                        return null;
                    }
                    catch (SqlException e)
                    {
                        return null;
                    }
                }
            }
        }

        public static String addUser(User user)
        {
            System.Console.Write(user.name);
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO [USER](Login, Name, Password, CreatedOn, IsActive, PicURL, IsAdmin)
                            VALUES(@param1,@param2,@param3,@param4,@param5,@param6,@param7)";

                    cmd.Parameters.AddWithValue("@param1", user.login);
                    cmd.Parameters.AddWithValue("@param2", user.name);
                    cmd.Parameters.AddWithValue("@param3", user.password);
                    cmd.Parameters.AddWithValue("@param4", user.createdOn);
                    cmd.Parameters.AddWithValue("@param5", user.isActive);
                    cmd.Parameters.AddWithValue("@param6", user.picURL);
                    cmd.Parameters.AddWithValue("@param7", user.isAdmin);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return "true";
                    }
                    catch (SqlException e)
                    {
                        return e.Message;
                    }

                }
            }
        }

        public static String addProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO Product(Name, TypeId, Price, Description, PicURL, UpdatedOn, UpdatedBy, IsActive)
                            VALUES(@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8)";

                    cmd.Parameters.AddWithValue("@param1", product.name);
                    cmd.Parameters.AddWithValue("@param2", product.typeId);
                    cmd.Parameters.AddWithValue("@param3", product.price);
                    cmd.Parameters.AddWithValue("@param4", product.description);
                    cmd.Parameters.AddWithValue("@param5", product.picURL);
                    cmd.Parameters.AddWithValue("@param6", product.updatedOn);
                    cmd.Parameters.AddWithValue("@param7", product.updatedBy);
                    cmd.Parameters.AddWithValue("@param8", product.isActive);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return "true";
                    }
                    catch (SqlException e)
                    {
                        return e.Message;
                    }

                }
            }
        }

        public static bool updateProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"UPDATE Product SET Name=@param1, TypeId=@param2, Price=@param3, Description=@param4, PicURL=@param5, UpdatedOn=@param6, UpdatedBy=@param7, IsActive=@param8 WHERE ProductId = @param9";

                    cmd.Parameters.AddWithValue("@param1", product.name);
                    cmd.Parameters.AddWithValue("@param2", product.typeId);
                    cmd.Parameters.AddWithValue("@param3", product.price);
                    cmd.Parameters.AddWithValue("@param4", product.description);
                    cmd.Parameters.AddWithValue("@param5", product.picURL);
                    cmd.Parameters.AddWithValue("@param6", product.updatedOn);
                    cmd.Parameters.AddWithValue("@param7", product.updatedBy);
                    cmd.Parameters.AddWithValue("@param8", product.isActive);
                    cmd.Parameters.AddWithValue("@param9", product.productId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException e)
                    {
                        return false;
                    }

                }
            }
        }

        public static Boolean updateUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"UPDATE [User] SET Login=@param1, Name=@param2, Password=@param3, PicURL=@param4 WHERE UserId = @param5";

                    cmd.Parameters.AddWithValue("@param1", user.login);
                    cmd.Parameters.AddWithValue("@param2", user.name);
                    cmd.Parameters.AddWithValue("@param3", user.password);
                    cmd.Parameters.AddWithValue("@param4", user.picURL);
                    cmd.Parameters.AddWithValue("@param5", user.userId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException e)
                    {
                        return false;
                    }

                }
            }
        }

        public static bool deleteProductById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"UPDATE Product SET IsActive = 0 WHERE ProductId = @param1";
                    cmd.Parameters.AddWithValue("@param1", id);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException e)
                    {
                        return false;
                    }

                }
            }
        }
    }
}
