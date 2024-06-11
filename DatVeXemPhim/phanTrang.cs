using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim
{
    public class phanTrang<T> : List<T>
    {
        public int soTrang { get; private set; }
        public int tongTrang { get; private set; }

        public phanTrang(List<T> items, int count, int pageIndex, int pageSize)
        {
            soTrang = pageIndex;
            tongTrang = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage => soTrang > 1;

        public bool HasNextPage => soTrang < tongTrang;

        public static async Task<phanTrang<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new phanTrang<T>(items, count, pageIndex, pageSize);
        }
    }
}