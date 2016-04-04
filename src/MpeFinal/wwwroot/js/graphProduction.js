/* graphProduction.js */
(function () {

	$.getJSON("/Gases/GetAllGasesDateOutput", function (data) {
		// Create the chart

		var highchartsOptions = Highcharts.setOptions({
			lang: {
				loading: 'Nalaga...',
				months: ['Januar', 'Februar', 'Marec', 'April', 'Maj', 'Junij', 'Julij', 'Avgust', 'September', 'Oktober', 'November', 'December'],
				weekdays: ['Nedelja', 'Ponedeljek', 'Torek', 'Sreda', 'Cetrtek', 'Petek', 'Sobota'],
				shortMonths: ['Jan', 'Feb', 'Mar', 'Apr', 'Maj', 'Jun', 'Jul', 'Avg', 'Sep', 'Okt', 'Nov', 'Dec'],
				exportButtonTitle: "Izvozi",
				printButtonTitle: "Natisni",
				printChart: "Natisni graf",
				rangeSelectorFrom: "Od",
				rangeSelectorTo: "Do",
				rangeSelectorZoom: "Obdobje",
				downloadPNG: 'Prenesi kot sliko PNG',
				downloadJPEG: 'Prenesi kot sliko JPEG',
				downloadPDF: 'Prenesi kot PDF',
				downloadSVG: 'Prenesi kot sliko SVG',
				decimalPoint: ','
			}
		});

		var chart = new Highcharts.StockChart({
			rangeSelector: {
				selected: 1
			},

			chart: {
				type: 'column',
				renderTo: 'contain'
			},
			title: {
				text: 'Proizvodnja'
			},

			credits: {
				enabled: false
			},
			series: [
				{
					name: 'Proizvedeno',
					data: data,
					tooltip: {
						valueDecimals: 2
					}
				}
			]
		});
	});


})();
