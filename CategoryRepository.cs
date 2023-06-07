using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace NimapTask.Models
{
    public class CategoryRepository
    {
        string strcon = ConfigurationManager.ConnectionStrings["TaskContext"].ConnectionString;
        SqlConnection con;

        public CategoryRepository()
        {
            con = new SqlConnection(strcon);
        }
        public List<CategoryMaster> ShowAllCategory()
        {
            List<CategoryMaster> lstCategory = new List<CategoryMaster>();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select CategoryID,CategoryName from CategoryMasters";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CategoryMaster categ = new CategoryMaster();
                categ.CategoryID = Convert.ToInt32(dr["CategoryID"].ToString());
                categ.CategoryName = dr["CategoryName"].ToString();
                lstCategory.Add(categ);
            }
            return lstCategory;
        }

        public DataSet GetCategory(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select CategoryID,CategoryName from CategoryMasters where CategoryID = @CategoryID";
            cmd.Parameters.AddWithValue("@CategoryID", id);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            return ds;
        }

        public void CreateCategory(string CatName)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into CategoryMasters(CategoryName) values(@CategoryName)", con);
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CategoryName", CatName);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateCategory(int CatId, string CatName)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update CategoryMasters set CategoryName=@CategoryName where CategoryID=@CategoryID", con);
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CategoryID", CatId);
            cmd.Parameters.AddWithValue("@CategoryName", CatName);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteCategory(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from CategoryMasters where CategoryID=@CategoryID", con);
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CategoryID", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}