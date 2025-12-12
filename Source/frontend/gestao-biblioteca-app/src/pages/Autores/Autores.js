import React, { useEffect, useState } from "react";
import Swal from 'sweetalert2';

import {
  Table, TableBody, TableCell, TableContainer, TableHead, TableRow,
  Paper, Button, Dialog, DialogActions, DialogContent, DialogTitle,
  TextField, FormControlLabel, Switch
} from "@mui/material";

import { listarAutores, criarAutor, atualizarAutor, deletarAutor } from "../../services/AutorService";

export default function Autores() {
  const [autores, DefinirAutores] = useState([]);
  const [loading, DefinirCarregamento] = useState(true);

  const [openModal, DefineAberturaModal] = useState(false);
  const [modalMode, DefineModoModal] = useState("criar");
  const [currentAutor, DefinirAutor] = useState({ nome: "", ativo: true });
  const [currentId, DefinirIdAutor] = useState(null);

  const AbrirModal = (modo, autor = { nome: "", ativo: true }, id = null) => {
    DefineModoModal(modo);
    DefinirAutor({ ...autor });
    DefinirIdAutor(id);
    DefineAberturaModal(true);
  };

  const FecharModal = () => {
    DefineAberturaModal(false);
    DefinirAutor({ nome: "", ativo: true });
    DefinirIdAutor(null);
  };

  const ObterAutores = async () => {
    try {
      DefinirCarregamento(true);
      const res = await listarAutores();
      if (res.success) DefinirAutores(res.data);
      DefinirCarregamento(false);
    } catch (error) {
      console.error("Erro na requisição:", error);
      DefinirCarregamento(false);
    }
  };

  useEffect(() => {
    ObterAutores();
  }, []);

  const SalvarAutor = async () => {
    try {
      let response;
      if (modalMode === "criar") {
        const { id, ...autorParaCriar } = currentAutor;
        response = await criarAutor(autorParaCriar);
      } else {
        response = await atualizarAutor(currentId, currentAutor);
      }

      if (response.success) {
        Swal.fire({
          icon: 'success',
          title: 'Sucesso!',
          text: response.data,
        });
      } else {
        Swal.fire({
          icon: 'error',
          title: 'Erro!',
          text: response.errors.join(', '),
        });
      }

      ObterAutores();
      FecharModal();
    } catch (error) {
      console.error("Um erro ocorreu ao salvar autor:", error);
      Swal.fire({
        icon: 'error',
        title: 'Erro!',
        text: 'Um erro ocorreu ao salvar o autor.',
      });
    }
  };

  const ExcluirAutor = async (id, nome) => {
  const result = await Swal.fire({
    title: `Deseja realmente excluir o autor "${nome}"?`,
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Sim, excluir',
    cancelButtonText: 'Cancelar'
  });

  if (result.isConfirmed) {
    try {

      const response = await deletarAutor(id);

      if (response.success) {
        Swal.fire({
          icon: 'success',
          title: 'Sucesso!',
          text: response.data,
        });

        ObterAutores();

      } else {
        Swal.fire({
          icon: 'error',
          title: 'Erro!',
          text: response.errors.join(', '),
        });
      }
    } catch (error) {
      console.error("Erro ao deletar autor:", error);
      Swal.fire({
        icon: 'error',
        title: 'Erro!',
        text: 'Ocorreu um erro ao deletar o autor.',
      });
    }
  }
};

  return (
    <div>
      <Button variant="contained" color="primary" onClick={() => AbrirModal("criar")}>
        Criar Autor
      </Button>

      <TableContainer component={Paper} sx={{ marginTop: 2 }}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Nome</TableCell>
              <TableCell>Ativo</TableCell>
              <TableCell>Ações</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {loading ? (
              <TableRow>
                <TableCell colSpan={3}>Carregando...</TableCell>
              </TableRow>
            ) : autores.length === 0 ? (
              <TableRow>
                <TableCell colSpan={3}>Nenhum autor encontrado.</TableCell>
              </TableRow>
            ) : (
              autores.map((autor) => (
                <TableRow key={autor.id}>
                  <TableCell>{autor.nome}</TableCell>
                  <TableCell>{autor.ativo ? "Sim" : "Não"}</TableCell>
                  <TableCell>
                    <Button size="small" onClick={() => AbrirModal("editar", autor, autor.id)}>Editar</Button>
                    <Button size="small" color="error" onClick={() => ExcluirAutor(autor.id, autor.nome)}>Excluir</Button>
                  </TableCell>
                </TableRow>
              ))
            )}
          </TableBody>
        </Table>
      </TableContainer>

      {/* Modal */}
      <Dialog open={openModal} onClose={FecharModal}>
        <DialogTitle>{modalMode === "criar" ? "Criar Autor" : "Editar Autor"}</DialogTitle>
        <DialogContent>
          <TextField
            label="Nome"
            value={currentAutor.nome}
            onChange={(e) => DefinirAutor({ ...currentAutor, nome: e.target.value })}
            fullWidth
            margin="dense"
          />
          <FormControlLabel
            control={
              <Switch
                checked={currentAutor.ativo}
                onChange={(e) => DefinirAutor({ ...currentAutor, ativo: e.target.checked })}
              />
            }
            label="Ativo"
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={FecharModal}>Cancelar</Button>
          <Button onClick={SalvarAutor} variant="contained" color="primary">
            Salvar
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}