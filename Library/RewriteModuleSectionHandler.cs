using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Web;

namespace Library
{
    public class RewriteModuleSectionHandler : IConfigurationSectionHandler
    {

        private XmlNode _XmlSection;
        private string _RewriteBase;
        private bool _RewriteOn;

        public XmlNode XmlSection
        {
            get { return _XmlSection; }
        }

        public string RewriteBase
        {
            get { return _RewriteBase; }
        }

        public bool RewriteOn
        {
            get { return _RewriteOn; }
        }
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            // set base path for rewriting module to
            // application root
            _RewriteBase = HttpContext.Current.Request.ApplicationPath;

            // process configuration section 
            // from web.config
            try
            {
                _XmlSection = section;
                _RewriteOn = Convert.ToBoolean(section.SelectSingleNode("rewriteOn").InnerText);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error while processing RewriteModule configuration section.", ex));
            }
            return this;
        }
    }
}
