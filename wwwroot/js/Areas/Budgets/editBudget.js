const EditBudget = () => {

    let totalBudget = 0;
    let budgets;

    let updatedTotalBudget;
    let budgetUpdates = {};

    const getBudgets = async () => {
        await new Promise((res) => $.ajax({
            url: "/Budgets/GetTableData", type: "GET", dataType: "json", success: function ({aaData: data}) {
                budgets = data.reduce((obj, item) => {
                    item.editedAmount = item.finalAmount;
                    obj[item.id] = item;
                    return obj;
                }, {});
                totalBudget = data.reduce((acc, curr) => acc + curr.amount, 0);
                updatedTotalBudget = data.reduce((acc, curr) => acc + curr.finalAmount, 0);
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
                }, {
                    data: "startDate", autoWidth: true,
                }, {
                    data: "endDate", autoWidth: true,
                }, {
                    data: "department.name", autoWidth: true,
                }, {
                    data: "finalAmount", autoWidth: true,
                    render: (value, _, data) => {
                        return `<span id="${data.id}">${value}</span>`;
                    }
                }, {
                    data: "finalAmount", autoWidth: true,
                    render: (value, _, data) => {
                        return `<span id="final_budget_${data.id}">${value}</span>`;
                    }
                }],
                "initComplete": () => {
                    // add total budget to the table
                    const totalBudgetRow = `<tr><td>Total Budget</td><td></td><td id="total_projected_budget">${totalBudget}</td><td></td><td></td><td></td><td id="total_budget">${updatedTotalBudget}</td><td id="total_final_budget">${updatedTotalBudget}</td></tr>`;
                    $('#budgets-table tbody').append(totalBudgetRow);
                }
            });
    }

    const onEditChanges = (budgetId, value) => {
        value = Number(value);
        updatedTotalBudget -= budgets[budgetId].editedAmount;
        budgetUpdates[budgetId] = {
            id: budgetId, amount: value, timestamp: new Date()
        };
        if (value === budgets[budgetId].finalAmount) {
            // remove the budget from the updated budgets
            delete budgetUpdates[budgetId];
        }
        updatedTotalBudget += value;
        budgets[budgetId].editedAmount = value;
        $(`#final_budget_${budgetId}`).text(value);
        $('#total_budget').text(updatedTotalBudget);
        $('#total_final_budget').text(updatedTotalBudget);
        // make the submit button disabled if the updated total budget is more than the total budget
        if (updatedTotalBudget > totalBudget) {
            $('#save-changes').prop('disabled', true);
        } else {
            $('#save-changes').prop('disabled', false);
        }
        return value;
    };

    const submitBudgetChanges = () => {
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
                totalBudget = updatedTotalBudget;
                budgetUpdates = {};
            }
        });
    };

    const makeEditable = () => {
        // make the amount column editable
        $('#budgets-table').on('click', 'td', function () {
            let colIndex = $(this).index();
            let budgetId = $(this)[0].children[0].id;
            console.log(colIndex, budgetId);
            if (colIndex === 6 && budgetId !== 'total_budget') {
                // get the edit deadline
                let editDeadline = budgets[budgetId].editDeadline;
                // parse the edit deadline from mssql format to js date format
                editDeadline = new Date(editDeadline);
                let today = new Date();
                if (today > editDeadline) {
                    return;
                }
                $(this).editable((value) => onEditChanges(budgetId, value));
            }
        });

    }

    return {
        bindDatatable, submitBudgetChanges, makeEditable
    }
}


$(document).ready(function () {
    const {bindDatatable, submitBudgetChanges, makeEditable} = EditBudget();
    bindDatatable();
    makeEditable();
    $('#save-changes').click(submitBudgetChanges);
});
