﻿namespace OCP5.Models.Entities;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual List<Vehicle> Vehicles { get; set; } = [];
    public virtual List<Model> Models { get; set; } = [];
}