using Microsoft.EntityFrameworkCore;

namespace entity_framework_lab_4.DAL;

public class Contexto: DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
    }
    
    public DbSet<Models.Curso> Cursos { get; set; }
    public DbSet<Models.Aluno> Alunos { get; set; } 
    public DbSet<Models.AlunosCursos> AlunosCursos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração da chave primária para a entidade AlunosCursos
        modelBuilder.Entity<Models.AlunosCursos>()
            .HasKey(ac => ac.AlunosCursosId);
        
        modelBuilder.Entity<Models.AlunosCursos>()
            .HasOne(ac => ac.Aluno)
            .WithMany(a => a.AlunosCursos)
            .HasForeignKey(ac => ac.AlunoId);

        modelBuilder.Entity<Models.AlunosCursos>()
            .HasOne(ac => ac.Curso)
            .WithMany(c => c.AlunosCursos)
            .HasForeignKey(ac => ac.CursoId);
    }
}