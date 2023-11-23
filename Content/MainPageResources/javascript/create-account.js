const inputField = document.getElementById('1');
const label = inputField.nextElementSibling;
inputField.addEventListener('input', (e) => {
  if (e.target.value !== '') {
    label.parentNode.classList.add('has-value');
  } else {
    label.parentNode.classList.remove('has-value');
  }
});

const inputField3 = document.getElementById('3');
const label3 = inputField3.nextElementSibling;
inputField3.addEventListener('input', (e) => {
  if (e.target.value !== '') {
    label3.parentNode.classList.add('has-value3');
  } else {
    label3.parentNode.classList.remove('has-value3');
  }
});
const inputField4 = document.getElementById('4');
const label4 = inputField4.nextElementSibling;
inputField4.addEventListener('input', (e) => {
  if (e.target.value !== '') {
    label4.parentNode.classList.add('has-value4');
  } else {
    label4.parentNode.classList.remove('has-value4');
  }
});