using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace NimapTask.Models
{
    public class ProductRepository
    {
        string strcon = ConfigurationManager.ConnectionStrings["TaskContext"].ConnectionString;
        SqlConnection con;

        public ProductRepository()
        {
            con = new SqlConnection(strcon);
        }

        public List<ProductMaster> ShowAllProduct()
        {
            List<ProductMaster> lstProduct = new List<ProductMaster>();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select p.ProductID,p.ProductName,p.CategoryID,c.CategoryName from ProductMasters p inner join CategoryMasters c on p.CategoryID = c.CategoryID";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ProductMaster product = new ProductMaster();
                CategoryMaster categ = new CategoryMaster();
                product.ProductId = Convert.ToInt32(dr["ProductID"].ToString());
                product.ProductName = dr["ProductName"].ToString();
                product.CategoryID = Convert.ToInt32(dr["CategoryID"].ToString());
                product.CategoryMaster = categ;
                product.CategoryMaster.CategoryName = dr["CategoryName"].ToString();
                lstProduct.Add(product);
            }
            return lstProduct;
        }

        public DataSet GetProduct(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select p.ProductID,p.ProductName,p.CategoryID,c.CategoryName from ProductMasters p inner join CategoryMasters c on p.CategoryID = c.CategoryID where ProductID = @ProductID";
            cmd.Parameters.AddWithValue("@ProductID", id);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            return ds;
        }
        public List<CategoryMaster> FillCategory()
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

        public void CreateProduct(int ProdID, string ProdName, object CatId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into ProductMasters(ProductName,CategoryID) values(@ProductName,@CategId)", con);
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ProductName", ProdName);
            cmd.Parameters.AddWithValue("@CategId", CatId);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateProduct(int ProdId, string ProdName, int CatId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update ProductMasters set ProductName=@ProductName,CategoryID=@CategId where ProductID=@ProductID", con);
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ProductID", ProdId);
            cmd.Parameters.AddWithValue("@ProductName", ProdName);
            cmd.Parameters.AddWithValue("@CategId", CatId);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteProduct(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from ProductMasters where ProductID=@ProductID", con);
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ProductID", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        
    }
}