function initRouteChart(categoriesData) {
    var categoryNames = Object.keys(categoriesData);
    var categoryQuantities = Object.values(categoriesData);
    var colors = ['rgb(255, 99, 132)', 'rgb(54, 162, 235)', 'rgb(255, 205, 86)', 'rgb(75, 192, 192)'];
    // Lấy thẻ canvas
    var ctx = document.getElementById('categoriesChart').getContext('2d');

    // Khởi tạo biểu đồ Doughnut
    var myDoughnutChart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: categoryNames,
            datasets: [
                {
                    data: categoryQuantities,
                    backgroundColor: colors
                }
            ]
        },
        options: {
            plugins: {
                legend: {
                    display: true,
                    position: 'left' // Đặt vị trí của chú thích
                }
            },
            tooltips: {
                callbacks: {
                    title: function (tooltipItem, data) {
                        return data.labels[tooltipItem[0].index];
                    },
                    label: function (tooltipItem, data) {
                        return 'Số đơn: ' + data.datasets[0].data[tooltipItem.index];
                    }
                }
            }
        }
    });
}
