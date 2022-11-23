const EditBudget = () => {

    let totalBudget = 0;
    let budgets;

    let updatedTotalBudget;
    let updatedBudgets;
    let budgetUpdates = {};

    const getBudgets = async () => {
        await new Promise((res) => $.ajax({
            url: "/Budgets/GetTableData", type: "GET", dataType: "json", success: function ({aaData: data}) {
                budgets = data.reduce((obj, item) => {
                    obj[item.id] = item;
                    return obj;
                }, {});
                totalBudget = data.reduce((acc, curr) => acc + curr.amount, 0);
                updatedTotalBudget = totalBudget;
                updatedBudgets = {...budgets};
                res();
            }
        }))
    };

    const bindDatatable = async () => {
        await getBudgets();
        $('#budgets-table')
            .DataTable({
                data: Object.values(budgets),
                bProcessing: true,
                bPaginate: false,
                bSearchable: false,
                dom: 'Bfrtip',
                buttons: ['excel', 'pdf'],
                columns: [{
                    data: "name", autoWidth: true, order: 'asc'
                }, {
                    data: "currency", autoWidth: true,
                }, {
                    data: "amount", autoWidth: true,
                    render: (value, _, data) => {
                        return `<span id="${data.id}">${value}</span>`;
                    }
                }, {
                    data: "startDate", autoWidth: true,
                }, {
                    data: "endDate", autoWidth: true,
                }, {
                    data: "department.name", autoWidth: true,
                },],
                "initComplete": () => {
                    // add total budget to the table
                    const totalBudgetRow = '<tr><td>Total Budget</td><td></td><td id="total_budget">' + totalBudget + '</td><td></td><td></td><td></td></tr>';
                    $('#budgets-table tbody').append(totalBudgetRow);
                }
            });
    }

    const onEditChanges = (budgetId, value) => {
        console.log(budgetId, value, budgets[budgetId]);
        value = Number(value);
        updatedTotalBudget -= updatedBudgets[budgetId].amount;
        budgetUpdates[budgetId] = {
            id: budgetId, amount: value, timestamp: new Date()
        };
        if (value === budgets[budgetId].amount) {
            // remove the budget from the updated budgets
            delete updatedBudgets[budgetId];
        }
        console.log(updatedTotalBudget);
        updatedBudgets[budgetId].amount = value;
        updatedTotalBudget += value;
        $('#total_budget').text(updatedTotalBudget);
        // make the submit button disabled if the updated total budget is more than the total budget
        if (updatedTotalBudget > totalBudget) {
            $('#save-changes').prop('disabled', true);
        } else {
            $('#save-changes').prop('disabled', false);
        }
        return value;
    };

    let submitBudgetChanges = () => {
        $.ajax({
            url: '/Budgets/Update',
            type: 'POST',
            data: JSON.stringify(Object.values(budgetUpdates)),
            contentType: 'application/json',
            dataType: 'json',
            success: function (result) {
                if (result.success) {
                    alert('Budgets updated successfully');
                }
                budgets = {...updatedBudgets};
                totalBudget = updatedTotalBudget;
                budgetUpdates = {};
            }
        });
    };

    return {
        bindDatatable, onEditChanges, submitBudgetChanges
    }
}


$(document).ready(function () {
    const {bindDatatable, onEditChanges, submitBudgetChanges} = EditBudget();
    bindDatatable();
    $('#save-changes').click(submitBudgetChanges);
    // make the amount column editable
    $('#budgets-table').on('click', 'td', function () {
        let colIndex = $(this).index();
        let budgetId = $(this)[0].children[0].id;
        console.log(colIndex, budgetId);
        if (colIndex === 2 && budgetId !== 'total_budget') {
            $(this).editable((value) => onEditChanges(budgetId, value), {
                type: 'text', placeholder: 'Enter new amount', tooltip: 'Click to edit...', style: 'display: inline'
            });
        }
    });
});
