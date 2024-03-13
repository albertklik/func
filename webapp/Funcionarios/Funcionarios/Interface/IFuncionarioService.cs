using Funcionarios.Model;

namespace Funcionarios.Interface;

public interface IFuncionarioService
{
    public Task<IEnumerable<Funcionario>> GetAll();
    public Task<Funcionario> Get(int codigo);
    public Task Save(Funcionario funcionario);
    public Task Update(Funcionario funcionario);
    public Task Delete(int codigo);
}