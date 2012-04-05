using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace hoachdinhtuonglai.Data.Core
{
   

    public class OptionCollection : List<Option>
    {
        public OptionCollection() { }
        public OptionCollection(IEnumerable<Option> list) : base(list) { }
    }

    public static class OptionDA
    {
        static Database petapoco = ConnectionPP.getConnection();
        static Cache cache = HttpContext.Current.Cache;

        public static OptionCollection SelectAll()
        {
            //try
            {
                //using (f8roomDataContext context = new f8roomDataContext())
                {
                    //var query = from item in context.Options
                    //            orderby item.ID descending
                    //            select item;
                    petapoco = ConnectionPP.getConnection();
                    OptionCollection lo = new OptionCollection(petapoco.Fetch<Option>("Select * from Options order by ID desc"));
                    petapoco.CloseSharedConnection();
                    return lo;
                }
            }
            // catch (Exception ex)
            {
                //ErrorLogDA.Insert(ex); 
                //ErrorLogDA.Insert(ex);
                //return new OptionCollection();
            }
        }


        public static Option SelectByID(long id)
        {
            //try
            {
                //using (f8roomDataContext context = new f8roomDataContext())
                {
                    petapoco = ConnectionPP.getConnection();
                    Option option = petapoco.FirstOrDefault<Option>("Select top (1) * from Options where [ID]=@0",id);
                    petapoco.CloseSharedConnection();
                    return option;

                }
            }
            // catch (Exception ex)
            {
                //ErrorLogDA.Insert(ex); throw new Exception( "Database Error");
                //return null;
            }
        }

        public static long Insert(Option Option)
        {
            //try
            {
                //using (f8roomDataContext context = new f8roomDataContext())
                {
                    petapoco = ConnectionPP.getConnection();
                    petapoco.Insert("Options","ID",true,Option);
                    petapoco.CloseSharedConnection();
                    return Option.ID;
                }
            }
            // catch (Exception ex)
            {
                //ErrorLogDA.Insert(ex); throw new Exception( "Database Error");
                //return -1;
            }
        }
        public static long Update(Option Option)
        {
            //try
            {
                //using (f8roomDataContext context = new f8roomDataContext())
                {
                    //Option original = SelectByID(Option.ID);

                    //context.Options.Attach(Option, original);
                    //context.SubmitChanges();
                    petapoco = ConnectionPP.getConnection();
                    petapoco.Update("Options", "ID", Option, Option.ID);
                    petapoco.CloseSharedConnection();
                    return Option.ID;
                }
            }
            // catch (Exception ex)
            {
                //ErrorLogDA.Insert(ex); 
                //throw ex;

            }
        }
        public static long Delete(Option Option)
        {
            //try
            {
                //using (f8roomDataContext context = new f8roomDataContext())
                {
                    //context.Options.Attach(Option, SelectByID(Option.ID));
                    //context.Options.DeleteOnSubmit(Option);
                    //context.SubmitChanges();
                    petapoco = ConnectionPP.getConnection();
                    petapoco.Delete("Options", "ID", Option, Option.ID);
                    petapoco.CloseSharedConnection();
                    return Option.ID;
                }
            }
            // catch (Exception ex)
            {
                //ErrorLogDA.Insert(ex); 
                throw new Exception("Database Error");

            }
        }


        public static int CountAll()
        {
            //try
            {
                petapoco = ConnectionPP.getConnection();
                int c = petapoco.ExecuteScalar<int>("select count(*) from Options");
                return c;
            }
            // catch (Exception ex)
            {
                //ErrorLogDA.Insert(ex);
                throw new Exception("Database Error");

            }
        }

        public static Option SelectByName(string p)
        {

            //using (f8roomDataContext context = new f8roomDataContext())
            {
                //context.DeferredLoadingEnabled = false;
                //var query = from item in context.Options
                //            where item.Name == p
                //            select item;
                petapoco = ConnectionPP.getConnection();
                var query = petapoco.Fetch<Option>("Select * from Options where [Name]=@0", p);

                if (query.Count() == 0)
                {
                    Option op = new Option();
                    op.autoload = "no";
                    op.Name = p;
                    op.Value = "1";
                    op.VfCMS_ID = 0;
                    OptionDA.Insert(op);
                    return op;
                }

                //foreach (var i in query)
                //{
                //    i.Value = (int.Parse(i.Value) + 1).ToString();
                //}
                //context.SubmitChanges();
                petapoco.CloseSharedConnection();
                return query.FirstOrDefault<Option>();


            }
        }

        public static Option SelectRandomByName(string p)
        {

            //using (f8roomDataContext context = new f8roomDataContext())
            {
                //var query = from item in context.Options
                //            orderby context.Random()
                //            where item.Name == p

                //            select item;
                petapoco = ConnectionPP.getConnection();
                var query = petapoco.Fetch<Option>("select * from Options where [Name]=@0",p);

                Random gen = new Random((int)DateTime.UtcNow.Ticks);

                int r = gen.Next(0, query.Count() - 1);
                if (r < 0)
                    r = 0;
                petapoco.CloseSharedConnection();
                return query.Skip(r).FirstOrDefault<Option>();

            }
        }
    }
}