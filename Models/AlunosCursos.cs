namespace entity_framework_lab_4.Models;

public class AlunosCursos
{
    public int AlunosCursosId { get; set; }
    
    public int AlunoId { get; set; }
    public Aluno? Aluno { get; set; }
    
    public int CursoId { get; set; }
    public Curso? Curso { get; set; }
}