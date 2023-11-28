// class PessoasRepository : IPessoasRepository
// {
//     List<Pessoas> pessoas = new List<Pessoas>
//     {
//         new Pessoas { PessoasId = 1, Nome = "Fulano", Email = "fulano@email.com", Senha = "123" },
//         new Pessoas { PessoasId = 2, Nome = "Ciclano", Email = "Ciclano@gmail.com", Senha = "456" }
//     };

//     public Pessoas? Login(string email, string senha)
//     {
//         var pessoa = pessoas.SingleOrDefault(p => p.Email == email && p.Senha == senha);
//         return pessoa;
//     }
// }