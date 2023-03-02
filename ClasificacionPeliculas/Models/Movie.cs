using System;
using System.Collections.Generic;

namespace ClasificacionPeliculas.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime ReleaseDate { get; set; }

    public int Duration { get; set; }

    public string? Director { get; set; }

    public string? Actors { get; set; }

    public string? Plot { get; set; }

    public decimal? Rating { get; set; }

    public int? Votes { get; set; }

    public string? PosterUrl { get; set; }

    public string? ImdbId { get; set; }

    public virtual ICollection<Moviescategory> Moviescategories { get; } = new List<Moviescategory>();
}
