﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DBSystem.BLL;
using DBSystem.ENTITIES;

namespace MatFSISExercisesCustom.Pages
{
    public partial class Exercise9and10 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel1.Text = "";
            if (!Page.IsPostBack)
            {
                BindList();
            }
        }
        protected void BindList()
        {
            try
            {
                PlayerController sysmgr = new PlayerController();
                List<PlayerEntity> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.LastName.CompareTo(y.LastName));
                List01.DataSource = info;
                List01.DataTextField = nameof(PlayerEntity.PlayerandID);
                List01.DataValueField = nameof(PlayerEntity.PlayerID);
                List01.DataBind();
                List01.Items.Insert(0, "select...");
            }
            catch (Exception ex)
            {
                MessageLabel1.Text = ex.Message;
            }
        }
        protected void Fetch_Click(object sender, EventArgs e)
        {
            if (List01.SelectedIndex == 0)
            {
                MessageLabel1.Text = "Select a Product";
            }
            else
            {
                try
                {
                    string productid = List01.SelectedValue;
                    Response.Redirect("CRUDPage.aspx?page=4&pid=" + productid + "&add=" + "no");
                }
                catch (Exception ex)
                {
                    MessageLabel1.Text = ex.Message;
                }
            }
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            try
            {
                string productid = List01.SelectedValue;
                Response.Redirect("CRUDPage.aspx?page=4&pid=" + productid + "&add=" + "yes");
            }
            catch (Exception ex)
            {
                MessageLabel1.Text = ex.Message;
            }
        }
    }
}