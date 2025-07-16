<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Estadistica.aspx.cs" Inherits="TPC_Clinica.Estadistica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <style>
        .estadisticas-container {
    display: flex;
    flex-wrap: wrap;
    gap: 30px;
    justify-content: center;
    margin-top: 30px;
}

.grafico-wrapper {
    display: flex;
    justify-content: center;
    align-items: center;
}


.grafico-wrapper:not(.barras) {
    width: 450px;
    height: 450px;
}


.grafico-wrapper.barras {
    width: 750px;
    height: 450px;
}


canvas {
    width: 100% !important;
    height: 100% !important;
    background-color: white;
    border: 1px solid #ccc;
    border-radius: 10px;
    padding: 15px;
    box-shadow: 0 0 10px rgba(0,0,0,0.1);
}

.titulo {
    text-align: center;
    margin-top: 30px;
}

.grafico {
    width: 100% !important;
    height: 100% !important;
}

.estadistica-extra {
    font-size: 1.2rem;
    color: #2c3e50;
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="titulo text-primary">Estadísticas de Turnos</h2>

    <asp:Literal ID="litEstados" runat="server" Mode="PassThrough" />
<asp:Literal ID="litEspecialidades" runat="server" Mode="PassThrough" />

    <div class="estadisticas-container">
    <div class="grafico-wrapper">
        <canvas id="chartEstados" class="grafico"></canvas>
    </div>
    <div class="grafico-wrapper barras">
        <canvas id="chartEspecialidades" class="grafico"></canvas>
    </div>

</div>
            <div class="estadistica-extra text-center mt-4">
    <asp:Literal ID="litMedicoMasCierres" runat="server" Mode="PassThrough" />
</div>

   <script>
       window.onload = function () {

           // =========================
           //  GRÁFICO DE TORTA (ESTADOS)
           // =========================

           // Calcula el total de turnos sumando los valores de cada estado
           const totalEstados = Object.values(dataEstados).reduce((a, b) => a + b, 0);

           // Prepara las etiquetas con el porcentaje correspondiente
           const labelsEstados = Object.keys(dataEstados).map(key => {
               const porcentaje = ((dataEstados[key] / totalEstados) * 100).toFixed(1);
               return `${key} (${porcentaje}%)`;
           });

           // Configura y renderiza el gráfico de torta
           const ctx1 = document.getElementById('chartEstados').getContext('2d');
           new Chart(ctx1, {
               type: 'pie',
               data: {
                   labels: labelsEstados,
                   datasets: [{
                       data: Object.values(dataEstados),
                       backgroundColor: ['#4CAF50', '#2196F3', '#FF9800', '#F44336', '#9C27B0', '#607D8B']
                   }]
               },
               options: {
                   responsive: true,
                   plugins: {
                       legend: {
                           position: 'bottom',
                           labels: {
                               font: {
                                   size: 14 // tamaño de texto de la leyenda
                               }
                           }
                       },
                       title: {
                           display: true,
                           text: 'Distribución por Estado',
                           font: {
                               size: 16
                           }
                       }
                   }
               }
           });

           // ================================
           //  GRÁFICO DE BARRAS (ESPECIALIDADES)
           // ================================

           // Calcula el total de turnos por especialidad
           const totalEspecialidades = Object.values(dataEspecialidades).reduce((a, b) => a + b, 0);

           // Calcula porcentajes y etiquetas
           const etiquetasEspecialidades = Object.keys(dataEspecialidades).map(key => {
               const porcentaje = ((dataEspecialidades[key] / totalEspecialidades) * 100).toFixed(1);
               return `${key} (${porcentaje}%)`;
           });

           // Configurar y renderizar el gráfico de barras
           const ctx2 = document.getElementById('chartEspecialidades').getContext('2d');
           new Chart(ctx2, {
               type: 'bar',
               data: {
                   labels: etiquetasEspecialidades,
                   datasets: [{
                       label: 'Turnos por Especialidad',
                       data: Object.values(dataEspecialidades),
                       backgroundColor: '#42A5F5'
                   }]
               },
               options: {
                   responsive: true,
                   scales: {
                       y: {
                           beginAtZero: true,
                           title: {
                               display: true,
                               text: 'Cantidad de Turnos'
                           }
                       },
                       x: {
                           ticks: {
                               maxRotation: 45,
                               minRotation: 45,
                               font: { size: 14 }
                           }
                       }
                   },
                   plugins: {
                       legend: {
                           position: 'top',
                           labels: {
                               font: { size: 14 }
                           }
                       },
                       title: {
                           display: true,
                           text: 'Turnos por Especialidad',
                           font: { size: 16 }
                       }
                   }
               }
           });
       };
   </script>
</asp:Content>
