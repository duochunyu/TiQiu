//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TiQiu.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class VERSION
    {
        public int ID { get; set; }
        public int OS { get; set; }
        public string VersionStr { get; set; }
        public int ForceUpdate { get; set; }
        public System.DateTime ReleaseDate { get; set; }
        public string ReleaseNote { get; set; }
        public string DownloadURL { get; set; }
    }
}
