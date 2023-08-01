﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvDb.Application.Actions.Genres.DataTransferObjects;
public class GenreDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public GenreDto() { }
    public GenreDto(int id, string name)
    {
        Id = id;
        Name = name;
    }
}