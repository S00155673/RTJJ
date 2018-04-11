using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.DAL
{
    public interface IPlayer<T> : IRepo<T>
    {
        T getCurrentPlayer();
       bool login(string username, string password);
    }
}
