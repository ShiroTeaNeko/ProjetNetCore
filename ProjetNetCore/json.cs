using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetNetCore
{
    class json
    {
        public string id { get; set; }
        public string title { get; set; }
        public string mediatype { get; set; }

        public override string ToString()
        {
            return $"id : {id}\n"+
                $"title : {title}\n"+
                $"media_type : {mediatype}";
        }
    }
}
