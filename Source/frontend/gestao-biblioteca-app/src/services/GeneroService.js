import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7252/Genero"
});

export const listarGeneros = async () => {
  const response = await api.get("/Listar");
  return response.data;
};

export const criarGenero = async (genero) => {
  const response = await api.post("/Criar", genero);
  return response.data;
};

export const editarGenero = async (id, genero) => {
  const response = await api.put(`/Editar/${id}`, genero);
  return response.data;
};

export const excluirGenero = async (id) => {
  const response = await api.delete(`/ExcluirLogicamente/${id}`);
  return response.data;
};