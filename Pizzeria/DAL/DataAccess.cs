using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;
using Pizzeria.ViewModel;

namespace Pizzeria.DAL
{
    public class DataAccess
    {

        //Sparar ny registrerad kund/konto till DB
        public void CreateNewCustomer(TomasosContext context, Kund newCustomer)
        {
            context.Add(newCustomer);
            context.SaveChanges();

        }

        //Sparar ändringar för kund/konto information till DB
        public void EditCustomer(TomasosContext context, KundViewModel editedCustomer, Kund currentCustomer)
        {
                context.SaveChanges();
            
        }

        //Hämtar/kontrollerar alla giltiga kund/konton från DB och returnerar till login metoden i UserController
        public List<Kund> GetAllCustomers(TomasosContext context)
        {
            var customerList = context.Kund;

            return customerList.ToList();
        }

        //Hämtar alla MattrattProdukt från DB och gör den till en lista
        public List<Matratt> GetAllMenuItems(TomasosContext context)
        {
            var menuList = context.Matratt.Include(x=>x.MatrattProdukt).ToList();

            foreach (var item in menuList)
            {
                item.MatrattProdukt = GetAllMatrattProdukts(context, item.MatrattId);
            }
            return menuList;
        }

        //När kunden/konto har laggt till en matträtt så väljer den att beställa den i kundvagnen då denna metod körs
        public void CreateOrderConnection(TomasosContext context, BestallningMatratt connection)
        {
            
            context.Add(connection);
            context.SaveChanges();

        }

        public void CreateOrder(TomasosContext context, Bestallning order)
        {
            context.Add(order);
            context.SaveChanges();
            
        }

        public Matratt GetSpecificMatratt(TomasosContext context, int foodId)
        {
            var selectedItem = context.Matratt.FirstOrDefault(x => x.MatrattId == foodId);
            return selectedItem;
        }

        public List<MatrattProdukt> GetAllMatrattProdukts(TomasosContext context,int matId)
        {
            var matrattprodukts = context.MatrattProdukt.Where(x => x.MatrattId == matId).Include(y=>y.Produkt).ToList();
            return matrattprodukts;
        }
    }
}
