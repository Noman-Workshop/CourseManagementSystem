const EditBudget = () => {

    let totalBudget = 0;
    let budgets;

    let updatedTotalBudget;
    let budgetUpdates = {};
    let canUpdateBudget = false;

    const getBudgets = async () => {
        await new Promise((res) => $.ajax({
            url: "/Budgets/GetTableData", type: "GET", dataType: "json", success: function ({aaData: data, canEdit}) {
                budgets = data.reduce((obj, item) => {
                    item.editedAmount = item.finalAmount;
                    obj[item.id] = item;
                    return obj;
                }, {});
                totalBudget = data.reduce((acc, curr) => acc + curr.amount, 0);
                updatedTotalBudget = data.reduce((acc, curr) => acc + curr.finalAmount, 0);
                canUpdateBudget = canEdit;
                res()
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
                    data: "finalAmount", autoWidth: true, render: (value, _, data) => {
                        return `<span id="${data.id}">${value}</span>`;
                    }
                }, {

                    data: "finalAmount", autoWidth: true, render: (value, _, data) => {
                        return `<span id="final_budget_${data.id}">${value}</span>`;
                    }
                }],
                columnDefs: [{
                    targets: 8,
                    render: function (_, type, data) {
                        return `<button class="btn btn-info btn-sm" id="edit_${data.id}">
                                            <i class="fas fa-pencil-alt">
                                            </i>
                                            Edit
                                        </button>`;
                    }
                }],
                "initComplete": () => {
                    let totalBudgetRow = `<tr><td>Total Budget</td><td></td><td id="total_projected_budget">${totalBudget}</td><td></td><td></td><td></td><td id="total_budget">${updatedTotalBudget}</td><td id="total_final_budget">${updatedTotalBudget}</td></tr>`;
                    if (!canUpdateBudget) {
                        totalBudgetRow = `<tr><td>Total Budget</td><td></td><td id="total_projected_budget">${totalBudget}</td><td></td><td></td><td></td><td id="total_final_budget">${updatedTotalBudget}</td></tr>`;
                    }
                    $('#budgets-table tbody').append(totalBudgetRow);
                }
            });
        onDatatableLoad();
    }

    const onDatatableLoad = () => {
        toastr.info('All data loaded')
        if (!canUpdateBudget) {
            // hide the save changes button
            $('#save-changes').hide();
            // hide the amount column
            $('#budgets-table').DataTable().column(6).visible(false);
        }
        $('#save-changes').click(submitBudgetChanges);
        // add event listener to edit button
        $('#budgets-table tbody').on('click', 'button', function () {
            let id = this.id.split('_')[1];
            makeItemEditable(id);
            // change the button to save button
            $(this).html(`<i class="fas fa-save"></i> Save`);
            $(this).removeClass('btn-info');
            $(this).addClass('btn-success');
            $(this).attr('id', `save_${id}`);
            // add event listener to the save button
            $(`#save_${id}`).on('click', function () {
                let value = $(`#input_${id}`).val();
                value = onEditChanges(id, value);
                // change the button to edit button
                $(this).html(`<i class="fas fa-pencil-alt"></i> Edit`);
                $(this).removeClass('btn-success');
                $(this).addClass('btn-info');
                $(this).attr('id', `edit_${id}`);
                // change the input field to a span
                let span = $(`<span id="${id}">${value}</span>`);
                $(`#input_${id}`).replaceWith(span);
            });
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
        console.log('budgetUpdates', budgetUpdates);
        toastr.info('Budget queued for update')
        return value;
    };

    const submitBudgetChanges = () => {
        if (Object.keys(budgetUpdates).length === 0) {
            toastr.info('No changes to save');
            return;
        }

        $.ajax({
            url: '/Budgets/Update',
            type: 'POST',
            data: JSON.stringify(Object.values(budgetUpdates)),
            contentType: 'application/json',
            dataType: 'json',
            complete: function (result) {
                if (result.status !== 200) {
                    toastr.error('Error updating budget');
                    return;
                }
                totalBudget = updatedTotalBudget;
                budgetUpdates = {};
                toastr.success('Budgets updated successfully');
            }
        });
    };

    const makeItemEditable = (id) => {
        console.log('makeItemEditable', id);
        let editDeadline = budgets[id].editDeadline;
        // parse the edit deadline from mssql format to js date format
        editDeadline = new Date(editDeadline);
        let today = new Date();
        if (today > editDeadline) {
            return;
        }
        // convert the column to an input field
        let column = $(`#${id}`);
        let input = $(`<input type="number" id="input_${id}" value="${column.text()}">`);
        column.replaceWith(input);
        // add event listener to the input field
    }

    const makeTableEditable = () => {
        if (!canUpdateBudget) return;
        $('#budgets-table').on('click', 'td', function () {
            let colIndex = $(this).index();
            let budgetId = $(this)[0].children[0].id;
            if (colIndex === 6 && budgetId !== 'total_budget') {
                makeItemEditable(budgetId);
            }
        });
    }

    return {
        bindDatatable
    }
}


$(document).ready(function () {
    const {bindDatatable} = EditBudget();
    bindDatatable();
});
