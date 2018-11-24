using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OidcSample.Models
{
    public class ScopeViewModel
    {
        public string Name { get; set; }
        /// <summary>
        /// 展示名
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否强调
        /// </summary>
        public bool Emphasize { get; set; }


        /// <summary>
        /// 是否必须
        /// </summary>
        public bool Required { get; set; }

        public bool Checked { get; set; }
    }
}
