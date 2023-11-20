function initChart(ctx, orderCounts) {
    // Tạo mảng nhãn và dữ liệu sau khi loại bỏ các ngày không có dữ liệu
    var labelsWithData = [];
    var dataWithValues = [];

    Object.keys(orderCounts).forEach(function (key) {
        if (orderCounts[key] !== 0) {
            labelsWithData.push(key);
            dataWithValues.push(orderCounts[key]);
        }
    });

     var myBarChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labelsWithData,
            datasets: [{
                label: 'Số lượng đơn',
                data: dataWithValues,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}
