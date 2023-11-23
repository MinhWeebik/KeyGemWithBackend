//const inputField = document.getElementById('1');
//const label = inputField.nextElementSibling;
//inputField.addEventListener('input', (e) => {
//    if (e.target.value !== '') {
//        label.parentNode.classList.add('has-value');
//    } else {
//        label.parentNode.classList.remove('has-value');
//    }
//});
//const inputField2 = document.getElementById('2');
//const label2 = inputField2.nextElementSibling;
//inputField2.addEventListener('input', (e) => {
//  if (e.target.value !== '') {
//    label2.parentNode.classList.add('has-value2');
//  } else {
//    label2.parentNode.classList.remove('has-value2');
//  }
//});
//const inputField3 = document.getElementById('3');
//const label3 = inputField3.nextElementSibling;
//inputField3.addEventListener('input', (e) => {
//  if (e.target.value !== '') {
//    label3.parentNode.classList.add('has-value3');
//  } else {
//    label3.parentNode.classList.remove('has-value3');
//  }
//});
//const inputField4 = document.getElementById('4');
//const label4 = inputField4.nextElementSibling;
//inputField4.addEventListener('input', (e) => {
//  if (e.target.value !== '') {
//    label4.parentNode.classList.add('has-value4');
//  } else {
//    label4.parentNode.classList.remove('has-value4');
//  }
//});
//const inputField5 = document.getElementById('5');
//const label5 = inputField5.nextElementSibling;
//inputField5.addEventListener('input', (e) => {
//  if (e.target.value !== '') {
//    label5.parentNode.classList.add('has-value5');
//  } else {
//    label5.parentNode.classList.remove('has-value5');
//  }
//});
//const inputField6 = document.getElementById('6');
//const label6 = inputField6.nextElementSibling;
//inputField6.addEventListener('input', (e) => {
//  if (e.target.value !== '') {
//    label6.parentNode.classList.add('has-value6');
//  } else {
//    label6.parentNode.classList.remove('has-value6');
//  }
//});
//const inputField7 = document.getElementById('7');
//const label7 = inputField7.nextElementSibling;
//inputField7.addEventListener('input', (e) => {
//  if (e.target.value !== '') {
//    label7.parentNode.classList.add('has-value7');
//  } else {
//    label7.parentNode.classList.remove('has-value7');
//  }
//});
//const inputField8 = document.getElementById('8');
//const label8 = inputField8.nextElementSibling;
//inputField8.addEventListener('input', (e) => {
//  if (e.target.value !== '') {
//    label8.parentNode.classList.add('has-value8');
//  } else {
//    label8.parentNode.classList.remove('has-value8');
//  }
//});
//const inputField9 = document.getElementById('9');
//const label9 = inputField9.nextElementSibling;
//inputField9.addEventListener('input', (e) => {
//  if (e.target.value !== '') {
//    label9.parentNode.classList.add('has-value9');
//  } else {
//    label9.parentNode.classList.remove('has-value9');
//  }
//});
//const input = document.getElementById('9');
//const button = document.getElementById('button1');

//input.addEventListener('input', function() {
//  if (input.value.trim() !== '') {
//    button.disabled = false;
//  } else {
//    button.disabled = true;
//  }
//});
//input.addEventListener('input', function() {
//  if (input.value.length > 0) {
//    button.classList.add('active');
//  } else {
//    button.classList.remove('active');
//  }
//});

let checkOutItemContainerEl = document.querySelector('.check-out-item-container');
let checkOut = JSON.parse(localStorage.getItem('CART'));
let subTotal = document.querySelector('.bill-sub2');
let total = document.querySelector('.bill-total2');

if (checkOut.length === 0) {
    window.location.href = '/mainpage';
}
function getCheckOutItem()
{
  let totalPrice = 0;
  let totalItems = 0;
  checkOutItemContainerEl.innerHTML = '';
  checkOut.forEach((item) => {
    checkOutItemContainerEl.innerHTML+=`
    <div class="check-out-item">
    <div>
          <img src="${item.variants.find(i => i.id === item.variantId).imageLink}" class="keycap-img" />
          </div>
          <div class="check-out-info-container">
          <div class="check-out-info-header-container">
            <p class="keycap-name">${item.name}</p>
             <p class="keycap-color">Amount: ${item.numberOfUnits}</p>
          </div>
          <div>
            <span class="keycap-cost">$${item.variants.find(i => i.id === item.variantId).price}</span>
          </div>
          </div>
    </div>
    `;
      totalPrice = totalPrice + (Number(item.variants.find(i => i.id === item.variantId).price) * Number(item.numberOfUnits));
      totalItems += Number(item.numberOfUnits);
  });
  subTotal.innerHTML = `$${totalPrice.toFixed(2)}`;
  total.innerHTML = `USD <strong>$${totalPrice.toFixed(2)}</strong>`
}

getCheckOutItem();

document.getElementById("checkOutButton").addEventListener("click", function () {
    var order = checkOut.map(function (item) {
        return {
            productId: item.id,
            variantId: item.variantId,
            quantity: parseInt(item.numberOfUnits),
            personId: personId
            };
        });

    order.forEach((item) => {
        fetch('https://localhost:44318/api/order', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(item)
        })
            .then(response => response.json())
            .then(data => {
                localStorage.clear();
                window.location.href = "/mainpage/ordercomplete";
            })
            .catch(error => {
                console.log("Error:", error);
            });
    })
    
});