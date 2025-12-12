import React, { useEffect, useState } from "react";
import Swal from "sweetalert2";

import {
  Table, TableBody, TableCell, TableContainer, TableHead, TableRow,
  Paper, Button, Dialog, DialogActions, DialogContent, DialogTitle,
  TextField, FormControlLabel, Switch, MenuItem, Select, InputLabel, FormControl
} from "@mui/material";

import { listarLivros, criarLivro, editarLivro, excluirLivro } from "../../services/LivroService";
import { listarAutores } from "../../services/AutorService";
import { listarGeneros } from "../../services/GeneroService";

export default function Livros() {

  const [livros, DefinirLivros] = useState([]);
  const [autores, DefinirAutores] = useState([]);
  const [generos, DefinirGeneros] = useState([]);

  const [loading, DefinirLoading] = useState(true);

  const [openModal, DefineAberturaModal] = useState(false);
  const [modalMode, DefineModoModal] = useState("criar"); 
  const [currentId, DefinirIdLivro] = useState(null);

  const [currentLivro, DefinirLivro] = useState({
    nome: "",
    idAutor: "",
    idGenero: "",
    ativo: true
  });

  const ObterLivros = async () => {
    try {
      DefinirLoading(true);
      const res = await listarLivros();
      if (res.success) DefinirLivros(res.data);
    } catch (error) {
      console.error("Erro ao carregar livros:", error);
    } finally {
      DefinirLoading(false);
    }
  };

  const ObterAutores = async () => {
    const res = await listarAutores();
    if (res.success) DefinirAutores(res.data);
  };

  const ObterGeneros = async () => {
    const res = await listarGeneros();
    if (res.success) DefinirGeneros(res.data);
  };

  useEffect(() => {
    ObterLivros();
    ObterAutores();
    ObterGeneros();
  }, []);

  const AbrirModal = (modo, livro = null) => {
    DefineModoModal(modo);

    if (modo === "editar" && livro) {
      DefinirIdLivro(livro.id);
      DefinirLivro({
        nome: livro.nome,
        idAutor: livro.idAutor,
        idGenero: livro.idGenero,
        ativo: livro.ativo
      });
    } else {
      DefinirIdLivro(null);
      DefinirLivro({
        nome: "",
        idAutor: "",
        idGenero: "",
        ativo: true
      });
    }

    DefineAberturaModal(true);
  };

  const FecharModal = () => {
    DefineAberturaModal(false);
    DefinirIdLivro(null);
    DefinirLivro({
      nome: "",
      idAutor: "",
      idGenero: "",
      ativo: true
    });
  };

  const SalvarLivro = async () => {
    try {
      let response;

      if (modalMode === "criar") {
        const livroParaCriar = { ...currentLivro };
        delete livroParaCriar.id;
        response = await criarLivro(livroParaCriar);

      } else {
        response = await editarLivro(currentId, currentLivro);
      }

      if (response.success) {
        Swal.fire({
          icon: "success",
          title: "Sucesso!",
          text: response.data
        });
      } else {
        Swal.fire({
          icon: "error",
          title: "Erro!",
          text: response.errors.join(", ")
        });
      }

      ObterLivros();
      FecharModal();

    } catch (error) {
      console.error("Erro ao salvar livro:", error);
      Swal.fire({
        icon: "error",
        title: "Erro!",
        text: "Ocorreu um erro ao salvar o livro."
      });
    }
  };

  const ExcluirLivro = async (id, nome) => {
    const result = await Swal.fire({
      title: `Deseja realmente excluir o livro "${nome}"?`,
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Sim, excluir",
      cancelButtonText: "Cancelar"
    });

    if (result.isConfirmed) {
      try {
        const response = await excluirLivro(id);

        if (response.success) {
          Swal.fire({
            icon: "success",
            title: "Sucesso!",
            text: response.data
          });
          ObterLivros();

        } else {
          Swal.fire({
            icon: "error",
            title: "Erro!",
            text: response.errors.join(", ")
          });
        }

      } catch (error) {
        console.error("Erro ao deletar livro:", error);
        Swal.fire({
          icon: "error",
          title: "Erro!",
          text: "Ocorreu um erro ao deletar o livro."
        });
      }
    }
  };

  return (
    <div>
      <Button variant="contained" color="primary" onClick={() => AbrirModal("criar")}>
        Criar Livro
      </Button>

      <TableContainer component={Paper} sx={{ marginTop: 2 }}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Nome</TableCell>
              <TableCell>Autor</TableCell>
              <TableCell>Gênero</TableCell>
              <TableCell>Ativo</TableCell>
              <TableCell>Ações</TableCell>
            </TableRow>
          </TableHead>

          <TableBody>
            {loading ? (
              <TableRow>
                <TableCell colSpan={5}>Carregando...</TableCell>
              </TableRow>
            ) : livros.length === 0 ? (
              <TableRow>
                <TableCell colSpan={5}>Nenhum livro encontrado.</TableCell>
              </TableRow>
            ) : (
              livros.map((livro) => (
                <TableRow key={livro.id}>
                  <TableCell>{livro.nome}</TableCell>
                  <TableCell>{livro.autor?.nome || "—"}</TableCell>
                  <TableCell>{livro.genero?.nome || "—"}</TableCell>
                  <TableCell>{livro.ativo ? "Sim" : "Não"}</TableCell>
                  <TableCell>
                    <Button size="small" onClick={() => AbrirModal("editar", livro)}>Editar</Button>
                    <Button size="small" color="error" onClick={() => ExcluirLivro(livro.id, livro.nome)}>Excluir</Button>
                  </TableCell>
                </TableRow>
              ))
            )}
          </TableBody>

        </Table>
      </TableContainer>

      {/* Modal */}
      <Dialog open={openModal} onClose={FecharModal}>
        <DialogTitle>{modalMode === "criar" ? "Criar Livro" : "Editar Livro"}</DialogTitle>

        <DialogContent>

          <TextField
            label="Nome"
            value={currentLivro.nome}
            onChange={(e) => DefinirLivro({ ...currentLivro, nome: e.target.value })}
            fullWidth
            margin="dense"
          />

          {/* Select Autor */}
          <FormControl fullWidth sx={{ marginTop: 2 }}>
            <InputLabel>Autor</InputLabel>
            <Select
              value={currentLivro.idAutor}
              label="Autor"
              onChange={(e) => DefinirLivro({ ...currentLivro, idAutor: e.target.value })}
            >
              {autores.map(a => (
                <MenuItem key={a.id} value={a.id}>{a.nome}</MenuItem>
              ))}
            </Select>
          </FormControl>

          {/* Select Genero */}
          <FormControl fullWidth sx={{ marginTop: 2 }}>
            <InputLabel>Gênero</InputLabel>
            <Select
              value={currentLivro.idGenero}
              label="Gênero"
              onChange={(e) => DefinirLivro({ ...currentLivro, idGenero: e.target.value })}
            >
              {generos.map(g => (
                <MenuItem key={g.id} value={g.id}>{g.nome}</MenuItem>
              ))}
            </Select>
          </FormControl>

          {/* Ativo */}
          <FormControlLabel
            control={
              <Switch
                checked={currentLivro.ativo}
                onChange={(e) => DefinirLivro({ ...currentLivro, ativo: e.target.checked })}
              />
            }
            label="Ativo"
            sx={{ marginTop: 2 }}
          />

        </DialogContent>

        <DialogActions>
          <Button onClick={FecharModal}>Cancelar</Button>
          <Button variant="contained" color="primary" onClick={SalvarLivro}>Salvar</Button>
        </DialogActions>

      </Dialog>

    </div>
  );
}