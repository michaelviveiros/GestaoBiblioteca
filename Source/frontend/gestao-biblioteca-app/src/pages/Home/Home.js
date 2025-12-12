import React, { useEffect, useState } from "react";
import { Grid, Card, CardContent, Typography, CardActionArea, CircularProgress, Box } from "@mui/material";
import { useNavigate } from "react-router-dom";

import { listarAutores } from "../../services/AutorService";
import { listarGeneros } from "../../services/GeneroService";
import { listarLivros } from "../../services/LivroService";

export default function Home() {
  const [totalAutores, definirTotalAutores] = useState(0);
  const [totalGeneros, definirTotalGeneros] = useState(0);
  const [totalLivros, definirTotalLivros] = useState(0);
  const [loading, definirCarregamento] = useState(true);

  const navigate = useNavigate();

  const fetchCounts = async () => {
    try {
      definirCarregamento(true);

      const [resAutores, resGeneros, resLivros] = await Promise.all([
        listarAutores(),
        listarGeneros(),
        listarLivros()
      ]);

      if (resAutores.success) definirTotalAutores(resAutores.data.length);
      if (resGeneros.success) definirTotalGeneros(resGeneros.data.length);
      if (resLivros.success) definirTotalLivros(resLivros.data.length);

    } catch (error) {
      console.error("Erro ao carregar contadores:", error);
    } finally {
      definirCarregamento(false);
    }
  };

  useEffect(() => {
    fetchCounts();
  }, []);

  if (loading) {
    return (
      <Box sx={{ display: "flex", justifyContent: "center", alignItems: "center", height: "80vh" }}>
        <CircularProgress />
      </Box>
    );
  }

  const cards = [
    { title: "Total de Autores", count: totalAutores, color: "#1976d2", route: "/autores" },
    { title: "Total de Gêneros", count: totalGeneros, color: "#388e3c", route: "/generos" },
    { title: "Total de Livros", count: totalLivros, color: "#f57c00", route: "/livros" },
  ];

  return (
    <Grid container spacing={3} sx={{ marginTop: 2 }}>
      {cards.map((card) => (
        <Grid item xs={12} sm={4} key={card.title}>
          <Card
            sx={{
              backgroundColor: card.color,
              color: "#fff",
              borderRadius: 3,
              boxShadow: 3,
              transition: "transform 0.2s",
              "&:hover": {
                transform: "scale(1.05)"
              }
            }}
          >
            <CardActionArea onClick={() => navigate(card.route)}>
              <CardContent sx={{ textAlign: "center", py: 4 }}>
                <Typography variant="h5" sx={{ fontWeight: "bold", mb: 1 }}>
                  {card.title}
                </Typography>
                <Typography variant="h3">{card.count}</Typography>
              </CardContent>
            </CardActionArea>
          </Card>
        </Grid>
      ))}
    </Grid>
  );
}