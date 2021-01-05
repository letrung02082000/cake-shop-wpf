﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ModelLib
{
    public class DatabaseAccess
    {
        // Cake CRUD
        public static List<CakeModel> LoadCake()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var output = connection.Query<CakeModel>($"Select * from cake", new DynamicParameters());
                return output.ToList();
            }
        }

        public static CakeModel FindCakeById(int cakeId)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var output = connection.QuerySingle<CakeModel>($"Select * from cake where cakeId = {cakeId}");
                return output;
            }
        }

        public static int SaveCake(CakeModel cake)
        {
            cake.CakeName2 = ConvertToUnSign(cake.CakeName);
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                int cakeId = connection.ExecuteScalar<int>("INSERT INTO cake(cakeCode, cakeName, cakeName2, cakeCat, cakePrice, cakeDesc, cakeImage, cakeQuantity) VALUES(@CakeCode, @CakeName, @CakeName2, @CakeCat, @CakePrice, @CakeDesc, @CakeImage, @CakeQuantity); SELECT last_insert_rowid()", cake);
                return cakeId;
            }
        }

        //Customer CRUD
        public static List<CustomerModel> LoadCustomer()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var output = connection.Query<CustomerModel>($"Select * from customer", new DynamicParameters());
                return output.ToList();
            }
        }

        public static CustomerModel FindCustomerById(int customerId)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var output = connection.QuerySingle<CustomerModel>($"Select * from customer where customerId = {customerId}");
                return output;
            }
        }

        public static int SaveCustomer(CustomerModel customer)
        {
            customer.CustomerName2 = ConvertToUnSign(customer.CustomerName);
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                int customId = connection.ExecuteScalar<int>("INSERT INTO customer(customerName, customerName2, customerTel) VALUES(@CustomerName, @CustomerName2, @CustomerTel); SELECT last_insert_rowid()", customer);
                return customId;
            }
        }

        private static string LoadConnectionString(string id = "default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2.ToLower();
        }
    }
}
