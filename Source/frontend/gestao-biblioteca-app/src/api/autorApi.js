import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7252/Autor"
});

export const listarAutores = async () => {
  const response = await api.get("/Listar");
  return response.data;
};

export const criarAutor = async (autor) => {
  const response = await api.post("/Criar", autor);
  return response.data;
};

export const atualizarAutor = async (id, autor) => {
  const response = await api.put(`/Editar/${id}`, autor);
  return response.data;
};

export const deletarAutor = async (id) => {
  const response = await api.delete(`/ExcluirLogicamente/${id}`);
  return response.data;
};