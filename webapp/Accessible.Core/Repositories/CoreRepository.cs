using AutoMapper;
using Accessible.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Accessible.Core.Repositories
{
    public class CoreRepository
    {
        private CoreDbContext _context = new CoreDbContext();
        private IMapper _mapper;

        public CoreRepository()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DTOs.Location, LocationEntity>().ReverseMap());
            _mapper = config.CreateMapper();

            _context.Database.Migrate();
        }

        public void Add(DTOs.Location location)
        {
            _context.LocationEntities.Add(Map(location));
            _context.SaveChanges();
        }

        public DTOs.Location Get(Guid id)
        {
            return Map(_context.LocationEntities.FirstOrDefault(l => l.Id == id));
        }

        public IEnumerable<DTOs.Location> Get(double minLat, double minLong, double maxLat, double maxLong)
        {
            return _context.LocationEntities.Where(l => l.Lat > minLat && l.Lat < maxLat && l.Long > minLong && l.Long < maxLong)
                .Select(l => Map(l)).ToArray();
        }

        public bool Delete(Guid id)
        {
            var loc = GetEntity(id);
            if (loc == null)
            {
                return false;
            }

            _context.LocationEntities.Remove(loc);
            _context.SaveChanges();
            return true;
        }

        private LocationEntity Map(DTOs.Location location)
        {
            return _mapper.Map<LocationEntity>(location);
        }

        private DTOs.Location Map(LocationEntity location)
        {
            return _mapper.Map<DTOs.Location>(location);
        }

        private LocationEntity GetEntity(Guid id)
        {
            return _context.LocationEntities.FirstOrDefault(l => l.Id == id);
        }

        public class CoreDbContext : DbContext
        {
            internal DbSet<LocationEntity> LocationEntities { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=accesible.db");
            }

#if DEBUG
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<LocationEntity>().HasData(new[]
                {
                    new LocationEntity { Id = Guid.NewGuid(), Name = "Bondi Beach", Lat = -33.890542, Long = 151.274856, Colour = "red" },
                    new LocationEntity { Id = Guid.NewGuid(), Name = "Coogee Beach", Lat = -33.923036, Long = 151.259052, Colour = "yellow" },
                    new LocationEntity { Id = Guid.NewGuid(), Name = "Cronulla Beach", Lat = -34.028249, Long = 151.157507, Colour = "green" },
                    new LocationEntity { Id = Guid.NewGuid(), Name = "Manly Beach", Lat = -33.800101, Long = 151.287478, Colour = "red" },
                    new LocationEntity { Id = Guid.NewGuid(), Name = "Maroubra Beach", Lat = -33.950198, Long = 151.259302, Colour = "yellow" }
                });
            }
#endif
        }
    }
}
