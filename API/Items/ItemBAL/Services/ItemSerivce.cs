using ItemBAL.DTO;
using ItemsDAL.Model;
using Microsoft.EntityFrameworkCore;
using ProductBLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductBLL.Services
{
    public class ItemSerivce : IDataRepository<ItemVM>
    {
        private readonly Context context;

        public ItemSerivce()
        {
            context = new Context();
        }
        public ItemSerivce(Context _context)
        { 
            this.context = _context;
        }

        public async void Add(ItemVM entity)
        {
            var item = new Item()
            {
                Title = entity.Title,
                Description=entity.Description
            };
          await context.Items.AddAsync(item);
            context.SaveChanges();
        }

        public async void Delete(int id)
        {
            var entity =await context.Items.FindAsync(id);
            context.Items.Remove(entity);
            context.SaveChanges();
        }

        public async Task<ItemVM> Get(int id)
        {
            var entity =await context.Items.FindAsync(id);
            return new ItemVM()
            {
             Title=entity.Title,
             Description=entity.Description,
              Id=entity.Id
            };
         
        }

        public async Task<IEnumerable<ItemVM>> GetAll()
        {
            var enttity =await context.Items.ToListAsync();
            return enttity.Select(e => new ItemVM()
            {
                Title=e.Title,
                Description=e.Description,
            

            }).ToList();
        }

        public async void Update(ItemVM dbEntity, int id)
        {
            var entity = context.Items.Find(id);
            entity.Title = dbEntity.Title;
            entity.Description = dbEntity.Description;
      
           await context.SaveChangesAsync();
        }
        

      


    }
}
