import axios from "axios";

// Cria uma instância do axios apontando para a base da API
const api = axios.create({
  baseURL: "https://localhost:7252/Livro"
});

export async function listarLivros() {
  return (await api.get("/Listar")).data;
}

export async function criarLivro(dto) {
  return (await api.post("/Criar", dto)).data;
}

export async function editarLivro(id, dto) {
  return (await api.put(`/Editar/${id}`, dto)).data;
}

export async function excluirLivro(id) {
  return (await api.delete(`/ExcluirLogicamente/${id}`)).data;
}
