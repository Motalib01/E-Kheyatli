﻿namespace kheyatli.Api.Dtos;

public class PortfolioDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid TailorId { get; set; }
}