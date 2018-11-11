using AutoMapper;
using Accessible.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using Accessible.Core.DTOs;
using Accessible.Core.Utils;

namespace Accessible.Core.Repositories
{
    public class CoreRepository
    {
        private CoreDbContext _context = new CoreDbContext();
        private IMapper _mapper;

        public CoreRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Location, LocationEntity>().ForMember(l => l.Lat, opt => opt.Ignore()).ForMember(l => l.Long, opt => opt.Ignore());
                cfg.CreateMap<LocationEntity, Location>().ForMember(l => l.Coordinate, opt => opt.Ignore());
            });

            _mapper = config.CreateMapper();

            _context.Database.Migrate();
        }

        private LocationEntity Map(Location location)
        {
            var result = _mapper.Map<LocationEntity>(location);
            result.Lat = location.Coordinate.Lat;
            result.Long = location.Coordinate.Lng;
            return result;
        }

        private Location Map(LocationEntity location)
        {
            var result = _mapper.Map<Location>(location);
            result.Coordinate = new Coordinate(location.Lat, location.Long);
            return result;
        }

        public void Add(Location location)
        {
            _context.LocationEntities.Add(Map(location));
            _context.SaveChanges();
        }

        public Location Get(Guid id)
        {
            return Map(_context.LocationEntities.FirstOrDefault(l => l.Id == id));
        }

        public IEnumerable<Location> Get(Rectangle rectangle)
        {
            return _context.LocationEntities.Where(l => rectangle.Contains(new Coordinate(l.Lat, l.Long)))
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
