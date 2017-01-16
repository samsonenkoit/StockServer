using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockServer.Models.Common.Error
{
    public class ErrorViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public ErrorViewModel()
        {

        }

        public ErrorViewModel(int id, string message)
        {
            Id = id;
            Message = message;
        }

        public ErrorViewModel(int Id): this(Id,null)
        {

        }
    }
}
