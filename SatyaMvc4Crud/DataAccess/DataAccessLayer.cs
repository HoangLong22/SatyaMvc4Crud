﻿using SatyaMvc4Crud.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SatyaMvc4Crud.DataAccess
{
    public class DataAccessLayer
    {
        public const int INSERT = 1;
        public const int UPDATE = 2;
        public const int DELETE = 3;
        public const int SHOWALL = 4;
        public const int SHOWBYID = 5;
        public string InsertData(Customer objcust)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", objcust.Name);
                cmd.Parameters.AddWithValue("@Address", objcust.Address);
                cmd.Parameters.AddWithValue("@Mobileno", objcust.Mobileno);
                cmd.Parameters.AddWithValue("@Birthdate", objcust.Birthdate);
                cmd.Parameters.AddWithValue("@EmailID", objcust.EmailID);
                cmd.Parameters.AddWithValue("@Query", INSERT);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public string UpdateData(Customer objcust)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", objcust.CustomerID);
                cmd.Parameters.AddWithValue("@Name", objcust.Name);
                cmd.Parameters.AddWithValue("@Address", objcust.Address);
                cmd.Parameters.AddWithValue("@Mobileno", objcust.Mobileno);
                cmd.Parameters.AddWithValue("@Birthdate", objcust.Birthdate);
                cmd.Parameters.AddWithValue("@EmailID", objcust.EmailID);
                cmd.Parameters.AddWithValue("@Query", UPDATE);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }
        public int DeleteData(String ID)
        {
            SqlConnection con = null;
            int result;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", ID);
                cmd.Parameters.AddWithValue("@Name", null);
                cmd.Parameters.AddWithValue("@Address", null);
                cmd.Parameters.AddWithValue("@Mobileno", null);
                cmd.Parameters.AddWithValue("@Birthdate", null);
                cmd.Parameters.AddWithValue("@EmailID", null);
                cmd.Parameters.AddWithValue("@Query", DELETE);
                con.Open();
                result = cmd.ExecuteNonQuery();
                return result;
            }
            catch
            {
                return result = 0;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Customer> Selectalldata()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<Customer> custlist = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", null);
                cmd.Parameters.AddWithValue("@Name", null);
                cmd.Parameters.AddWithValue("@Address", null);
                cmd.Parameters.AddWithValue("@Mobileno", null);
                cmd.Parameters.AddWithValue("@Birthdate", null);
                cmd.Parameters.AddWithValue("@EmailID", null);
                cmd.Parameters.AddWithValue("@Query", SHOWALL);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                custlist = new List<Customer>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Customer cobj = new Customer();
                    cobj.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"].ToString());
                    cobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    cobj.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    cobj.Mobileno = ds.Tables[0].Rows[i]["Mobileno"].ToString();
                    cobj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                    cobj.Birthdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Birthdate"].ToString());

                    custlist.Add(cobj);
                }
                return custlist;
            }
            catch
            {
                return custlist;
            }
            finally
            {
                con.Close();
            }
        }

        public Customer SelectDatabyID(string CustomerID)
        {;
            SqlConnection con = null;
            DataSet ds = null;
            Customer cobj = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@Name", null);
                cmd.Parameters.AddWithValue("@Address", null);
                cmd.Parameters.AddWithValue("@Mobileno", null);
                cmd.Parameters.AddWithValue("@Birthdate", null);
                cmd.Parameters.AddWithValue("@EmailID", null);
                cmd.Parameters.AddWithValue("@Query", SHOWBYID);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cobj = new Customer();
                    cobj.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"].ToString());
                    cobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    cobj.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    cobj.Mobileno = ds.Tables[0].Rows[i]["Mobileno"].ToString();
                    cobj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                    cobj.Birthdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Birthdate"].ToString());
                }
                return cobj;
            }
            catch
            {
                return cobj;
            }
            finally
            {
                con.Close();
            }
        }
    }
}