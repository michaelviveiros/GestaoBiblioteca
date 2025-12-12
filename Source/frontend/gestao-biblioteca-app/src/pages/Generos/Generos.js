import React, { useEffect, useState } from "react";
import Swal from "sweetalert2";

import {
  Table, TableBody, TableCell, TableContainer, TableHead, TableRow,
  Paper, Button, Dialog, DialogActions, DialogContent, DialogTitle,
  TextField, FormControlLabel, Switch
} from "@mui/material";

import { listarGeneros, criarGenero, editarGenero, excluirGenero } from "../../services/GeneroService";

export default function Generos() {
  const [generos, DefinirGeneros] = useState([]);
  const [loading, DefinirCarregamento] = useState(true);

  const [openModal, DefineAberturaModal] = useState(false);
  const [modalMode, DefineModoModal] = useState("criar");
  const [currentGenero, DefinirGenero] = useState({ nome: "", ativo: true });
  const [currentId, DefinirIdGenero] = useState(null);

  const AbrirModal = (modo, genero = { nome: "", ativo: true }, id = null) => {
    DefineModoModal(modo);
    DefinirGenero({ ...genero });
    DefinirIdGenero(id);
    DefineAberturaModal(true);
  };

  const FecharModal = () => {
    DefineAberturaModal(false);
    DefinirGenero({ nome: "", ativo: true });
    DefinirIdGenero(null);
  };

  const ObterGeneros = async () => {
    try {
      DefinirCarregamento(true);
      const res = await listarGeneros();
      if (res.success) DefinirGeneros(res.data);
      DefinirCarregamento(false);
    } catch (error) {
      console.error("Erro ao carregar gêneros:", error);
      DefinirCarregamento(false);
    }
  };

  useEffect(() => {
    ObterGeneros();
  }, []);

  const SalvarGenero = async () => {
    try {
      let response;

      if (modalMode === "criar") {
        const { id, ...generoParaCriar } = currentGenero;
        response = await criarGenero(generoParaCriar);
      } else {
        response = await editarGenero(currentId, currentGenero);
      }

      if (response.success) {
        Swal.fire({
          icon: "success",
          title: "Sucesso!",
          text: response.data,
        });
      } else {
        Swal.fire({
          icon: "error",
          title: "Erro!",
          text: response.errors.join(", "),
        });
      }

      ObterGeneros();
      FecharModal();

    } catch (error) {
      console.error("Erro ao salvar gênero:", error);
      Swal.fire({
        icon: "error",
        title: "Erro!",
        text: "Um erro ocorreu ao salvar o gênero.",
      });
    }
  };

  const ExcluirGenero = async (id, nome) => {
    const result = await Swal.fire({
      title: `Deseja excluir o gênero "${nome}"?`,
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Sim, excluir",
      cancelButtonText: "Cancelar"
    });

    if (result.isConfirmed) {
      try {
        const response = await excluirGenero(id);

        if (response.success) {
          Swal.fire({
            icon: "success",
            title: "Sucesso!",
            text: response.data,
          });
          ObterGeneros();
        } else {
          Swal.fire({
            icon: "error",
            title: "Erro!",
            text: response.errors.join(", "),
          });
        }
      } catch (error) {
        console.error("Erro ao deletar gênero:", error);
        Swal.fire({
          icon: "error",
          title: "Erro!",
          text: "Ocorreu um erro ao deletar o gênero.",
        });
      }
    }
  };

  return (
    <div>
      <Button variant="contained" color="primary" onClick={() => AbrirModal("criar")}>
        Criar Gênero
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
            ) : generos.length === 0 ? (
              <TableRow>
                <TableCell colSpan={3}>Nenhum gênero encontrado.</TableCell>
              </TableRow>
            ) : (
              generos.map((genero) => (
                <TableRow key={genero.id}>
                  <TableCell>{genero.nome}</TableCell>
                  <TableCell>{genero.ativo ? "Sim" : "Não"}</TableCell>
                  <TableCell>
                    <Button size="small" onClick={() => AbrirModal("editar", genero, genero.id)}>Editar</Button>
                    <Button size="small" color="error" onClick={() => ExcluirGenero(genero.id, genero.nome)}>Excluir</Button>
                  </TableCell>
                </TableRow>
              ))
            )}
          </TableBody>

        </Table>
      </TableContainer>

      <Dialog open={openModal} onClose={FecharModal}>
        <DialogTitle>{modalMode === "criar" ? "Criar Gênero" : "Editar Gênero"}</DialogTitle>
        <DialogContent>
          <TextField
            label="Nome"
            value={currentGenero.nome}
            onChange={(e) => DefinirGenero({ ...currentGenero, nome: e.target.value })}
            fullWidth
            margin="dense"
          />
          <FormControlLabel
            control={
              <Switch
                checked={currentGenero.ativo}
                onChange={(e) => DefinirGenero({ ...currentGenero, ativo: e.target.checked })}
              />
            }
            label="Ativo"
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={FecharModal}>Cancelar</Button>
          <Button onClick={SalvarGenero} variant="contained" color="primary">
            Salvar
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}