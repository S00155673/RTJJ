using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.DAL
{
    public interface IRepo<T>
    {

        List<T> getEntities();
        T getEntity(int id);
        bool PutEntity(T entity);

        bool PostEntity(T entity);

    }
}
