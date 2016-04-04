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
			],

			rangeSelector: {
			  selected: 1,
			  buttonSpacing: 30,
			  buttons: [{
			    type: 'week',
			    count: 1,
			    text: '1t',
			    width: 150
			  }, {
			    type: '1m',
			    count: 1,
			    text: '1m'
			  }, {
			    type: 'month',
			    count: 3,
			    text: '3m '
			  }, {
			    type: 'month',
			    count: 6,
			    text: '6m '
			  }, {
			    type: 'year',
			    count: 1,
			    text: '1l '
			  }, {
			    type: 'ytd',
			    count: 3,
			    text: 'ldd'
			  }]
			}
		});
	});


})();
