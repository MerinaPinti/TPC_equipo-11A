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
        canvas {
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
    max-width: 100%;
    height: auto;
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="titulo text-primary">Estadísticas de Turnos</h2>

    <asp:Literal ID="litEstados" runat="server" Mode="PassThrough" />
<asp:Literal ID="litEspecialidades" runat="server" Mode="PassThrough" />

    <div class="estadisticas-container">
        <canvas id="chartEstados" width="350" height="350" class="grafico"></canvas>
<canvas id="chartEspecialidades" width="350" height="350" class="grafico"></canvas>
    </div>

    <script>
       
            
            window.onload = function () {
                console.log("dataEstados", dataEstados);
                console.log("dataEspecialidades", dataEspecialidades);

            const ctx1 = document.getElementById('chartEstados').getContext('2d');
            new Chart(ctx1, {
                type: 'pie',
                data: {
                    labels: Object.keys(dataEstados),
                    datasets: [{
                        data: Object.values(dataEstados),
                        backgroundColor: ['#4CAF50', '#2196F3', '#FF9800', '#F44336', '#9C27B0', '#607D8B']
                    }]
                },
                options: {
                    responsive: true,
                    plugins: { legend: { position: 'bottom' } },
                    font: {
                        size: 14 
                    },
                    title: { display: true, text: 'Distribución por Estado' }
                }
            });

            const ctx2 = document.getElementById('chartEspecialidades').getContext('2d');
            new Chart(ctx2, {
                type: 'bar',
                data: {
                    labels: Object.keys(dataEspecialidades),
                    datasets: [{
                        label: 'Turnos por Especialidad',
                        data: Object.values(dataEspecialidades),
                        backgroundColor: '#42A5F5'
                    }]
                },
                options: {
                    responsive: true,
                    scales: { y: { beginAtZero: true } }
                }
            });
        };
    </script>
</asp:Content>
