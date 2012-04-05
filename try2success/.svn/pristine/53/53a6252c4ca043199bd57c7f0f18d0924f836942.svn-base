using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace hoachdinhtuonglai.Data.Core
{
    //public class Category
    //{
    //    private int _ID;

    //    public int ID
    //    {
    //        get { return _ID; }
    //        set { _ID = value; }
    //    }

    //    private string _CateName;

    //    public string CateName
    //    {
    //        get { return _CateName; }
    //        set { _CateName = value; }
    //    }

    //    private string _CateSlugName;

    //    public string CateSlugName
    //    {
    //        get { return _CateSlugName; }
    //        set { _CateSlugName = value; }
    //    }

    //    private System.Nullable<int> _Order;

    //    public System.Nullable<int> Order
    //    {
    //        get { return _Order; }
    //        set { _Order = value; }
    //    }

    //    private System.Nullable<bool> _Active;

    //    public System.Nullable<bool> Active
    //    {
    //        get { return _Active; }
    //        set { _Active = value; }
    //    }
    //}


    public class CategoryCollection : List<Category>
    {
        public CategoryCollection() { }
        public CategoryCollection(IEnumerable<Category> list) : base(list) { }
    }

    [DataObject]
    public static class CategoryDA
    {
        static Database petapoco;
        public static CategoryCollection SelectAll()
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            CategoryCollection acc = new CategoryCollection(petapoco.Fetch<Category>(@"Select * from Categories")); petapoco.CloseSharedConnection();
            return acc;
        }

        public static CategoryCollection SelectAllNonActive()
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            CategoryCollection acc = new CategoryCollection(petapoco.Fetch<Category>(@"Select * from Categories where Activate=0")); petapoco.CloseSharedConnection();
            return acc;
        }



        public static Category SelectByID(long id)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Category ac = petapoco.FirstOrDefault<Category>(@"Select * from Categories where id=@0", id);
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static CategoryCollection SelectByCateNameSlug(string name)
        {
            name = name.Trim().ToLower();
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            CategoryCollection ac = new CategoryCollection(petapoco.Fetch<Category>(@"Select * from Categories where CateSlugName=@0", name));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static Category SelectByCateName(string name)
        {

            petapoco = ConnectionPP.getConnection();

            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Category ac = (petapoco.FirstOrDefault<Category>(@"Select * from Categories where CateName=@0", name));
            petapoco.CloseSharedConnection();
            return ac;
        }


        public static CategoryCollection SelectCategorySByPageSize(int page, int pagesize)
        {
            if (page == 0)
                page = 1;
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            CategoryCollection acc = new CategoryCollection(petapoco.Fetch<Category>(@"Select * from Categories where Activate=1").Skip((page - 1) * pagesize).Take(page * pagesize));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static long Update(Category Category)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            long x = petapoco.Update("Categories", "ID", Category, Category.ID); petapoco.CloseSharedConnection();
            return x;
        }

        public static long Insert(Category Category)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;

            petapoco.ForceDateTimesToUtc = false;

            var x = petapoco.Insert("Categories", "ID", true, Category);
            petapoco.CloseSharedConnection();
            return long.Parse(x.ToString());
        }

        public static long Delete(Category Category)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;
            Category.Active = false;
            string[] cols = { "Active" };
            long x = petapoco.Update("Categories", "ID", Category, Category.ID, cols);
            petapoco.CloseSharedConnection();
            return x;
        }
    }
}
