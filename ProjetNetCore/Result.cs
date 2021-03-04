using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetNetCore
{
    public class Result
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Media_type { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"id : {Id}\n" +
                $"title : {Title}{Name}\n" +
                $"media_type : {Media_type}\n";
        }
    }
}
