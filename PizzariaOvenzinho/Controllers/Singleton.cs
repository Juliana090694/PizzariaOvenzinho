using PizzariaOvenzinho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzariaOvenzinho.Controllers
{
    internal class Singleton
    {

        private static readonly Singleton instance = new Singleton();
        private readonly Entities entities;

        private Singleton()
        {
            entities = new Entities();
        }

        public static Singleton Instance
        {
            get
            {
                return instance;
            }
        }

        public Entities Entities
        {
            get
            {
                return entities;
            }
        }
    }
}