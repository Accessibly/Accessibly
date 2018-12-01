using AutoMapper;
using Accessible.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using Accessible.Core.DTOs;
using Accessible.Core.Utils;
using System.IO;
using CsvHelper.Configuration;

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
                cfg.CreateMap<Location, LocationEntity>()
                .ForMember(l => l.Lat, opt => opt.Ignore())
                .ForMember(l => l.Long, opt => opt.Ignore())
                .ForMember(l => l.Category, opt => opt.Ignore());
                cfg.CreateMap<LocationEntity, Location>()
                .ForMember(l => l.Coordinate, opt => opt.Ignore())
                .ForMember(l => l.Colour, opt => opt.Ignore());
            });

            _mapper = config.CreateMapper();

            _context.Database.Migrate();

            if (!_context.LocationEntities.Any())
            {
                SeedData();
            }
        }

        private void SeedData()
        {
            using (var contents = new StreamReader("../Data/seedData.csv"))
            using (var reader = new CsvHelper.CsvReader(contents))
            {
                reader.Configuration.RegisterClassMap<LocationEntityMap>();
                reader.Configuration.PrepareHeaderForMatch = header => header.ToLower();
                var items = reader.GetRecords<LocationEntity>();

                _context.LocationEntities.AddRange(items);

                _context.SaveChanges();
            }
        }

        private LocationEntity Map(Location location)
        {
            var result = _mapper.Map<LocationEntity>(location);
            result.Lat = location.Coordinate.Lat;
            result.Long = location.Coordinate.Lng;

            switch (location.Colour)
            {
                case "green":
                    result.Category = Category.Accessible;
                    break;
                case "red":
                    result.Category = Category.NotAccessible;
                    break;
                case "yellow":
                    result.Category = Category.PartiallyAccessible;
                    break;
            }

            return result;
        }

        private Location Map(LocationEntity location)
        {
            var result = _mapper.Map<Location>(location);
            result.Coordinate = new Coordinate(location.Lat, location.Long);

            switch (location.Category)
            {
                case Category.Accessible:
                    result.Colour = "green";
                    break;
                case Category.Unknown:
                    result.Colour = "red";
                    break;
                case Category.NotAccessible:
                    result.Colour = "red";
                    break;
                case Category.PartiallyAccessible:
                    result.Colour = "yellow";
                    break;
            }

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
        }

        private sealed class LocationEntityMap : ClassMap<LocationEntity>
        {
            public LocationEntityMap()
            {
                AutoMap();
                Map(l => l.Id).Ignore();
            }
        }
    }
}
