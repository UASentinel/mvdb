﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvDb.Application.Actions.AgeRatings.DataTransferObjects;
public class AgeRatingDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte MinAge { get; set; }
    public AgeRatingDto() { }
    public AgeRatingDto(int id, string name, byte minAge)
    {
        Id = id;
        Name = name;
        MinAge = minAge;
    }
}
