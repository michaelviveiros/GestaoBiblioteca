import axios from "axios";

// Cria uma instância do axios apontando para a base da API
const api = axios.create({
  baseURL: "https://localhost:7252/Autor"
});

// Função para listar todos os autores
export const listarAutores = async () => {
  const response = await api.get("/Listar");
  return response.data;
};

// Função para criar um novo autor
export const criarAutor = async (autor) => {
  const response = await api.post("/Criar", autor);
  return response.data;
};

// Função para atualizar um autor existente
export const atualizarAutor = async (id, autor) => {
  const response = await api.put(`/Editar/${id}`, autor);
  return response.data;
};

// Função para deletar um autor (logicamente)
export const deletarAutor = async (id) => {
  const response = await api.delete(`/ExcluirLogicamente/${id}`);
  return response.data;
};