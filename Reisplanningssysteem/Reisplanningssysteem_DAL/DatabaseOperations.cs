﻿using Microsoft.EntityFrameworkCore;
using Reisplanningssysteem_Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_DAL
{
    public class DatabaseOperations
    {
        

        public static List<Gebruiker> OphalenLijstGebruikers()
        {
            using (ReisplanningssysteemContext ctx = new ReisplanningssysteemContext())
            {
                return ctx.Gebruikers.
                Include(x => x.Gemeente).ToList();
            }
        }

        public static List<Gemeente> OphalenGemeentes()
        {
            using (ReisplanningssysteemContext ctx = new ReisplanningssysteemContext())
            {
                return ctx.Gemeenten.ToList();
            }
        }


        public static int VerwijderGebruiker(Gebruiker gebruiker)
        {
            try
            {
                using ReisplanningssysteemContext ctx = new();
                ctx.Entry(gebruiker).State = EntityState.Deleted;
                return ctx.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int VoegGebruikerToe(Gebruiker gebruikerRecord)
        {
            try
            {
                using ReisplanningssysteemContext ctx = new();
                ctx.Gebruikers.Add(gebruikerRecord);
                return ctx.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int UpdateGebruiker(Gebruiker gebruiker)
        {
            try
            {
                using ReisplanningssysteemContext ctx = new();
                ctx.Entry(gebruiker).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static Gebruiker ZoekGebruikerOpId(int gebruikerId)
        {
            using (ReisplanningssysteemContext ctx = new ReisplanningssysteemContext())
            {
                return ctx.Gebruikers
                    .Where(x => x.Id == gebruikerId).First();
            }
        }

    }
}
