﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKA.Krivosinnyy.DAL.Entities
{
    internal class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public enum Gender;
        public enum Relationship;
        public DateOnly BirthDate { get; set; }
        public int? FatherId { get; set; }
        public Person? Father { get; set; }
        public int? MotherId { get; set; }
        public Person? Morther { get; set; }
    }
    public enum Gender { Man, Woman }
    public enum Relationship
    {
        Son,
        Daughter,
        Father,
        Mother,
        Uncle,
        Aunt,
        Nethew,
        Niece,
        Cousin,
        Grandfather,
        Grandmother,
        Groom,
        Bride,
        Husband,
        Wife,
        Friend,
        Girlfriend
    }
}