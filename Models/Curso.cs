namespace entity_framework_lab_4.Models;

public class Curso
{
    public int CursoId { get; set; }
    public string Nome { get; set; }
    
    public ICollection<AlunosCursos> AlunosCursos { get; set; }
}